import { map, delay, cache } from "../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/Seq.js";
import { rangeDouble } from "../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/Range.js";
import { createObj, getEnumerator } from "../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/Util.js";
import { Union, Record } from "../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/Types.js";
import { union_type, list_type, record_type, bool_type, string_type, int32_type } from "../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/Reflection.js";
import { append, singleton as singleton_1, filter, length, map as map_1, ofArray } from "../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/List.js";
import { Cmd_none } from "../build/fable_modules/Fable.Elmish.4.0.0/cmd.fs.js";
import { singleton } from "../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/AsyncBuilder.js";
import { sleep } from "../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/Async.js";
import { Cmd_OfAsync_start, Cmd_OfAsyncWith_perform } from "../build/fable_modules/Fable.Elmish.4.0.0/cmd.fs.js";
import { createElement } from "react";
import { join } from "../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/String.js";
import { reactApi } from "../build/fable_modules/Feliz.2.9.0/Interop.fs.js";
import { ProgramModule_mkProgram, ProgramModule_run } from "../build/fable_modules/Fable.Elmish.4.0.0/program.fs.js";
import { Program_withReactSynchronous } from "../build/fable_modules/Fable.Elmish.React.4.0.0/react.fs.js";

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

export function init() {
    const cards = ofArray([Card_makeCard_Z721C83C5("fudge.png"), Card_makeCard_Z721C83C5("fudge.png"), Card_makeCard_Z721C83C5("fudge.png"), Card_makeCard_Z721C83C5("fudge.png")]);
    return [new State(0, [new Game(cards)]), Cmd_none()];
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

export function shakeCards(cards) {
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

export function selectedCount(cards) {
    return length(filter((x) => x.selected, cards));
}

export function update(msg, state) {
    if (state.tag === 0) {
        const gameState = state.fields[0];
        if (msg.tag === 0) {
            const card = msg.fields[0];
            const newCards = selectCard(card, gameState.cardsToGuess);
            if (selectedCount(newCards) === 3) {
                const effect = () => singleton.Delay(() => singleton.Bind(sleep(2400), () => singleton.Return(undefined)));
                const afterEffect = () => (new Message(1, []));
                const shakenCards = shakeCards(newCards);
                return [new State(0, [new Game(shakenCards)]), Cmd_OfAsyncWith_perform((x) => {
                    Cmd_OfAsync_start(x);
                }, effect, undefined, afterEffect)];
            }
            else {
                return [new State(0, [new Game(newCards)]), Cmd_none()];
            }
        }
        else {
            throw new Error("Match failure");
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
        const cardClasses = () => {
            const res = c.selected ? ofArray(["card", "card-selected"]) : singleton_1("card");
            if (c.shake) {
                return append(res, singleton_1("shake-card"));
            }
            else {
                return res;
            }
        };
        return createElement("div", createObj(ofArray([["className", join(" ", cardClasses())], (elems = [createElement("img", {
            src: `/public/${c.image}`,
            alt: "Card",
            onClick: (x) => {
                dispatch(new Message(0, [c]));
            },
        })], ["children", reactApi.Children.toArray(Array.from(elems))])])));
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

