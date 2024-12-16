import { Union } from "./fable_modules/fable-library-js.4.24.0/Types.js";
import { string_type, char_type, union_type } from "./fable_modules/fable-library-js.4.24.0/Reflection.js";
import { createElement } from "react";
import { curry2 } from "./fable_modules/fable-library-js.4.24.0/Util.js";
import { ofArray } from "./fable_modules/fable-library-js.4.24.0/List.js";
import { reactApi } from "./fable_modules/Feliz.2.9.0/Interop.fs.js";
import { ProgramModule_mkSimple, ProgramModule_run } from "./fable_modules/Fable.Elmish.4.0.0/program.fs.js";
import { Program_withReactSynchronous } from "./fable_modules/Fable.Elmish.React.4.0.0/react.fs.js";

export class Opearacija extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Add", "Sub", "Mul", "Div"];
    }
}

export function Opearacija_$reflection() {
    return union_type("Program.Opearacija", [], Opearacija, () => [[], [], [], []]);
}

export class Poruka extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Broj", "Jednako", "Clear", "Operator"];
    }
}

export function Poruka_$reflection() {
    return union_type("Program.Poruka", [], Poruka, () => [[["Item", char_type]], [], [], [["Item", Opearacija_$reflection()]]]);
}

export class State extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["UnosimPrviBroj", "UnioOperator", "UnioDrugiBroj"];
    }
}

export function State_$reflection() {
    return union_type("Program.State", [], State, () => [[["Item", string_type]], [["Item", string_type]], [["Item", string_type]]]);
}

export function init() {
    return new State(0, [""]);
}

export function update(msg, state) {
    if (msg.tag === 0) {
        const c = msg.fields[0];
        return new State(0, [c]);
    }
    else {
        return new State(0, ["X"]);
    }
}

export function render(state, dispatch) {
    const clickFoo = (broj, me) => {
        dispatch(new Poruka(0, [broj]));
    };
    const brojBtn = (broj_1) => createElement("button", {
        className: "broj-btn",
        children: broj_1,
        onClick: curry2(clickFoo)(broj_1),
    });
    const opBtn = (op) => createElement("button", {
        className: "op-btn",
        children: op,
    });
    const eqBtn = createElement("button", {
        className: "eq-btn",
        children: "=",
    });
    const clrBtn = createElement("button", {
        className: "clr-btn",
        children: "C",
    });
    const br = createElement("br", {});
    let text;
    switch (state.tag) {
        case 1: {
            const s_1 = state.fields[0];
            text = ("op -> " + s_1);
            break;
        }
        case 2: {
            const s_2 = state.fields[0];
            text = ("b2 -> " + s_2);
            break;
        }
        default: {
            const s = state.fields[0];
            text = ("b1 -> " + s);
        }
    }
    const display = createElement("div", {
        children: text,
    });
    const children = ofArray([display, brojBtn("1"), brojBtn("2"), brojBtn("3"), opBtn("+"), br, brojBtn("4"), brojBtn("5"), brojBtn("6"), opBtn("-"), br, brojBtn("7"), brojBtn("8"), brojBtn("9"), opBtn("*"), br, clrBtn, brojBtn("0"), eqBtn, opBtn("/")]);
    return createElement("div", {
        children: reactApi.Children.toArray(Array.from(children)),
    });
}

ProgramModule_run(Program_withReactSynchronous("app", ProgramModule_mkSimple(init, update, render)));

