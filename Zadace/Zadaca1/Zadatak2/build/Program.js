import { Union } from "./fable_modules/fable-library-js.4.24.0/Types.js";
import { string_type, char_type, union_type } from "./fable_modules/fable-library-js.4.24.0/Reflection.js";
import { createObj, curry2, int64ToString } from "./fable_modules/fable-library-js.4.24.0/Util.js";
import { compare, op_Division, fromInt32, equals, op_Multiply, op_Subtraction, op_Addition, toInt64 } from "./fable_modules/fable-library-js.4.24.0/BigInt.js";
import { parse } from "./fable_modules/fable-library-js.4.24.0/Long.js";
import { createElement } from "react";
import { reactApi } from "./fable_modules/Feliz.2.9.0/Interop.fs.js";
import { ofArray } from "./fable_modules/fable-library-js.4.24.0/List.js";
import { ProgramModule_mkSimple, ProgramModule_run } from "./fable_modules/Fable.Elmish.4.0.0/program.fs.js";
import { Program_withReactSynchronous } from "./fable_modules/Fable.Elmish.React.4.0.0/react.fs.js";

export class Operation extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Add", "Sub", "Mul", "Div", "Invalid"];
    }
}

export function Operation_$reflection() {
    return union_type("Program.Operation", [], Operation, () => [[], [], [], [], []]);
}

export class Msg extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Number", "Evaluate", "Clear", "Operator"];
    }
}

export function Msg_$reflection() {
    return union_type("Program.Msg", [], Msg, () => [[["Item", char_type]], [], [], [["Item", Operation_$reflection()]]]);
}

export class State extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["InputFirstNumber", "GotOperator", "InputSecondNumber"];
    }
}

export function State_$reflection() {
    return union_type("Program.State", [], State, () => [[["Item", string_type]], [["Item1", string_type], ["Item2", Operation_$reflection()]], [["Item1", string_type], ["Item2", Operation_$reflection()], ["Item3", string_type]]]);
}

export function strToOp(str) {
    const matchValue = str.toLocaleLowerCase();
    switch (matchValue) {
        case "+":
            return new Operation(0, []);
        case "-":
            return new Operation(1, []);
        case "*":
            return new Operation(2, []);
        case "/":
            return new Operation(3, []);
        case "add":
            return new Operation(0, []);
        case "sub":
            return new Operation(1, []);
        case "mul":
            return new Operation(2, []);
        case "div":
            return new Operation(3, []);
        default:
            return new Operation(4, []);
    }
}

export function charToOp(ch) {
    return strToOp(ch);
}

export function opToChar(op) {
    switch (op.tag) {
        case 1:
            return "-";
        case 2:
            return "*";
        case 3:
            return "/";
        case 4:
            return "?";
        default:
            return "+";
    }
}

export function opToStr(op) {
    return opToChar(op);
}

export function eval$(lhs, op, rhs) {
    const result = ((lhs === "NaN") ? true : (rhs === "NaN")) ? "NaN" : (((lhs === "-∞") ? true : (rhs === "-∞")) ? "-∞" : (((lhs === "∞") ? true : (rhs === "∞")) ? "∞" : ((op.tag === 0) ? int64ToString(toInt64(op_Addition(toInt64(parse(lhs, 511, false, 64)), toInt64(parse(rhs, 511, false, 64))))) : ((op.tag === 1) ? int64ToString(toInt64(op_Subtraction(toInt64(parse(lhs, 511, false, 64)), toInt64(parse(rhs, 511, false, 64))))) : ((op.tag === 2) ? int64ToString(toInt64(op_Multiply(toInt64(parse(lhs, 511, false, 64)), toInt64(parse(rhs, 511, false, 64))))) : ((op.tag === 4) ? "Invalid operation" : (equals(toInt64(parse(rhs, 511, false, 64)), toInt64(fromInt32(0))) ? "NaN" : int64ToString(toInt64(op_Division(toInt64(parse(lhs, 511, false, 64)), toInt64(parse(rhs, 511, false, 64))))))))))));
    if (result.length <= 10) {
        return result;
    }
    else if (compare(toInt64(parse(result, 511, false, 64)), toInt64(fromInt32(0))) < 0) {
        return "-∞";
    }
    else {
        return "∞";
    }
}

export const initState = new State(0, [""]);

export function init() {
    return initState;
}

export function update(msg, state) {
    switch (state.tag) {
        case 1: {
            const op_1 = state.fields[1];
            const num = state.fields[0];
            switch (msg.tag) {
                case 1:
                    return state;
                case 2:
                    return initState;
                case 3: {
                    const newOp = msg.fields[0];
                    return new State(1, [num, newOp]);
                }
                default: {
                    const n_1 = msg.fields[0];
                    return new State(2, [num, op_1, n_1]);
                }
            }
        }
        case 2: {
            const rhs = state.fields[2];
            const op_2 = state.fields[1];
            const lhs = state.fields[0];
            switch (msg.tag) {
                case 1:
                    return new State(0, [eval$(lhs, op_2, rhs)]);
                case 2:
                    return initState;
                case 3: {
                    const newOp_1 = msg.fields[0];
                    return new State(1, [eval$(lhs, op_2, rhs), newOp_1]);
                }
                default: {
                    const n_2 = msg.fields[0];
                    if (rhs.length < 10) {
                        return new State(2, [lhs, op_2, rhs + n_2]);
                    }
                    else {
                        return state;
                    }
                }
            }
        }
        default: {
            const str = state.fields[0];
            switch (msg.tag) {
                case 1:
                    return state;
                case 2:
                    return initState;
                case 3: {
                    const op = msg.fields[0];
                    return new State(1, [str, op]);
                }
                default: {
                    const n = msg.fields[0];
                    if (str.length < 10) {
                        return new State(0, [str + n]);
                    }
                    else {
                        return state;
                    }
                }
            }
        }
    }
}

export function render(state, dispatch) {
    let elems;
    const click_num = (num, _arg) => {
        dispatch(new Msg(0, [num]));
    };
    const click_op = (op, _arg_1) => {
        dispatch(new Msg(3, [charToOp(op)]));
    };
    const click_eq = (_arg_2) => {
        dispatch(new Msg(1, []));
    };
    const click_clr = (_arg_3) => {
        dispatch(new Msg(2, []));
    };
    const btn_num = (num_1) => createElement("button", {
        className: "btn-num",
        children: num_1,
        onClick: curry2(click_num)(num_1),
    });
    const btn_op = (op_1) => createElement("button", {
        className: "btn-op",
        children: op_1,
        onClick: curry2(click_op)(op_1),
    });
    const btn_eq = createElement("button", {
        className: "btn-eq",
        children: "=",
        onClick: click_eq,
    });
    const btn_clr = createElement("button", {
        className: "btn-clr",
        children: "C",
        onClick: click_clr,
    });
    const br = createElement("br", {});
    let text;
    switch (state.tag) {
        case 1: {
            const op_2 = state.fields[1];
            const num_2 = state.fields[0];
            text = ((num_2 + " ") + opToStr(op_2));
            break;
        }
        case 2: {
            const rhs = state.fields[2];
            const op_3 = state.fields[1];
            const lhs = state.fields[0];
            text = ((((lhs + " ") + opToStr(op_3)) + " ") + rhs);
            break;
        }
        default: {
            const s = state.fields[0];
            text = s;
        }
    }
    const display = createElement("div", {
        className: "display",
        children: text,
    });
    return createElement("div", createObj(ofArray([["className", "container"], (elems = [display, btn_num("1"), btn_num("2"), btn_num("3"), btn_op("+"), br, btn_num("4"), btn_num("5"), btn_num("6"), btn_op("-"), br, btn_num("7"), btn_num("8"), btn_num("9"), btn_op("*"), br, btn_clr, btn_num("0"), btn_eq, btn_op("/")], ["children", reactApi.Children.toArray(Array.from(elems))])])));
}

ProgramModule_run(Program_withReactSynchronous("app", ProgramModule_mkSimple(init, update, render)));

