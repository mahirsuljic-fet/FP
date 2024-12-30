import { map, delay, cache } from "./fable_modules/fable-library-js.4.24.0/Seq.js";
import { rangeDouble } from "./fable_modules/fable-library-js.4.24.0/Range.js";
import { createObj, getEnumerator } from "./fable_modules/fable-library-js.4.24.0/Util.js";
import { Union, Record } from "./fable_modules/fable-library-js.4.24.0/Types.js";
import { union_type, list_type, record_type, bool_type, string_type, int32_type } from "./fable_modules/fable-library-js.4.24.0/Reflection.js";
import { append, singleton as singleton_1, ofArray, filter, length, map as map_1 } from "./fable_modules/fable-library-js.4.24.0/List.js";
import { Cmd_none } from "./fable_modules/Fable.Elmish.4.0.0/cmd.fs.js";
import { singleton } from "./fable_modules/fable-library-js.4.24.0/AsyncBuilder.js";
import { sleep } from "./fable_modules/fable-library-js.4.24.0/Async.js";
import { Cmd_OfAsync_start, Cmd_OfAsyncWith_perform } from "./fable_modules/Fable.Elmish.4.0.0/cmd.fs.js";
import { createElement } from "react";
import { join } from "./fable_modules/fable-library-js.4.24.0/String.js";
import { reactApi } from "./fable_modules/Feliz.2.9.0/Interop.fs.js";
import { ProgramModule_mkProgram, ProgramModule_run } from "./fable_modules/Fable.Elmish.4.0.0/program.fs.js";
import { Program_withReactSynchronous } from "./fable_modules/Fable.Elmish.React.4.0.0/react.fs.js";

export const IDSEQ = cache(delay(() => map((i) => i, rangeDouble(1, 1, 200000))));

export const nextID = (() => {
    const enum$ = getEnumerator(IDSEQ);
    const foo = () => {
        if (enum$["System.Collections.IEnumerator.MoveNext"]()) {
            return enum$["System.Collections.Generic.IEnumerator`1.get_Current"]() | 0;
        }
        else {
            throw new Error("No more ids.");
        }
    };
    return foo;
})();

export class Card extends Record {
    constructor(id, image, selected, shake, guessed) {
        super();
        this.id = (id | 0);
        this.image = image;
        this.selected = selected;
        this.shake = shake;
        this.guessed = guessed;
    }
}

export function Card_$reflection() {
    return record_type("Program.Card", [], Card, () => [["id", int32_type], ["image", string_type], ["selected", bool_type], ["shake", bool_type], ["guessed", bool_type]]);
}

export function Card_makeCard_Z721C83C5(image) {
    return new Card(nextID(), image, false, false, false);
}

export class Game extends Record {
    constructor(cardsToGuess) {
        super();
        this.cardsToGuess = cardsToGuess;
    }
}

export function Game_$reflection() {
    return record_type("Program.Game", [], Game, () => [["cardsToGuess", list_type(Card_$reflection())]]);
}

export class State extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Initial", "InitialGoodGuess", "InitialWrongGuess", "Guessed1"];
    }
}

export function State_$reflection() {
    return union_type("Program.State", [], State, () => [[["Item", Game_$reflection()]], [["Item", Game_$reflection()]], [["Item", Game_$reflection()]], [["Item", Game_$reflection()]]]);
}

export class Message extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["TileClick", "DeselectAll"];
    }
}

export function Message_$reflection() {
    return union_type("Program.Message", [], Message, () => [[["Item", Card_$reflection()]], []]);
}

export function selectCard(card, cards) {
    const foo = (c) => {
        if (c.id === card.id) {
            return new Card(c.id, c.image, true, c.shake, c.guessed);
        }
        else {
            return c;
        }
    };
    return map_1(foo, cards);
}

export function shakeCard(cards) {
    const foo = (c) => {
        if (c.selected) {
            return new Card(c.id, c.image, c.selected, true, c.guessed);
        }
        else {
            return c;
        }
    };
    return map_1(foo, cards);
}

export function deselectCards(cards) {
    const foo = (c) => (new Card(c.id, c.image, false, c.shake, c.guessed));
    return map_1(foo, cards);
}

export function countSelected(cards) {
    return length(filter((x) => x.selected, cards));
}

export function init() {
    const cards = ofArray([Card_makeCard_Z721C83C5("fudge.png"), Card_makeCard_Z721C83C5("fudge.png"), Card_makeCard_Z721C83C5("fudge.png"), Card_makeCard_Z721C83C5("fudge.png")]);
    return [new State(0, [new Game(cards)]), Cmd_none()];
}

export function update(msg, state) {
    if (state.tag === 0) {
        const gameState = state.fields[0];
        if (msg.tag === 1) {
            const newCards_1 = shakeCard(deselectCards(gameState.cardsToGuess));
            return [new State(0, [new Game(newCards_1)]), Cmd_none()];
        }
        else {
            const card = msg.fields[0];
            const newCards = selectCard(card, gameState.cardsToGuess);
            if (countSelected(gameState.cardsToGuess) < 3) {
                return [new State(0, [new Game(newCards)]), Cmd_none()];
            }
            else {
                const shakenCards = shakeCard(newCards);
                const wait2sec = () => singleton.Delay(() => singleton.Bind(sleep(2000), () => singleton.Return(undefined)));
                const cmdfoo = () => (new Message(1, []));
                return [new State(0, [new Game(shakenCards)]), Cmd_OfAsyncWith_perform((x) => {
                    Cmd_OfAsync_start(x);
                }, wait2sec, undefined, cmdfoo)];
            }
        }
    }
    else {
        return [state, Cmd_none()];
    }
}

export function render(state, dispatch) {
    let elems_3;
    const card = (c) => {
        let elems;
        let cardClasses;
        const res = singleton_1("card");
        const res1 = c.selected ? append(res, singleton_1("selected-card")) : res;
        cardClasses = (c.shake ? append(res1, singleton_1("shake-card")) : res1);
        return createElement("div", createObj(ofArray([["className", join(" ", cardClasses)], (elems = [createElement("img", {
            src: `/public/${c.image}`,
            alt: "Card",
        })], ["children", reactApi.Children.toArray(Array.from(elems))]), ["onClick", (_arg) => {
            dispatch(new Message(0, [c]));
        }]])));
    };
    const cards = () => {
        let c_2, c_3, c_4, c_1;
        const guessingCards = map_1(card, (state.tag === 1) ? ((c_2 = state.fields[0], c_2.cardsToGuess)) : ((state.tag === 2) ? ((c_3 = state.fields[0], c_3.cardsToGuess)) : ((state.tag === 3) ? ((c_4 = state.fields[0], c_4.cardsToGuess)) : ((c_1 = state.fields[0], c_1.cardsToGuess)))));
        return guessingCards;
    };
    const grid = () => {
        let elems_1;
        return createElement("div", createObj(ofArray([["className", "grid"], (elems_1 = cards(), ["children", reactApi.Children.toArray(Array.from(elems_1))])])));
    };
    return createElement("div", createObj(ofArray([["className", "container"], (elems_3 = [grid()], ["children", reactApi.Children.toArray(Array.from(elems_3))])])));
}

ProgramModule_run(Program_withReactSynchronous("app", ProgramModule_mkProgram(init, update, render)));

