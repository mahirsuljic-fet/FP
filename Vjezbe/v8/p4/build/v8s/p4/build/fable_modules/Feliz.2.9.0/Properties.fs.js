import { join } from "../../../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/String.js";
import { length, ofArray, map } from "../../../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/List.js";
import { milliseconds, seconds, minutes, hours } from "../../../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/TimeSpan.js";
import { map as map_1 } from "../../../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/Seq.js";

export function PropHelpers_createClockValue(duration) {
    let i_1;
    return (join(":", map((i) => ((i < 10) ? ("0" + i) : i), ofArray([hours(duration), minutes(duration), seconds(duration)]))) + ".") + ((i_1 = (milliseconds(duration) | 0), (i_1 < 10) ? ("0" + i_1) : i_1));
}

export function PropHelpers_createKeySplines(values) {
    return join("; ", map_1((tupledArg) => {
        const x1 = tupledArg[0];
        const y1 = tupledArg[1];
        const x2 = tupledArg[2];
        const y2 = tupledArg[3];
        return (((((x1 + " ") + y1) + " ") + x2) + " ") + y2;
    }, values));
}

export function PropHelpers_createOnKey(key, handler, ev) {
    const patternInput = key;
    const shift = patternInput[2];
    const pressedKey = patternInput[0];
    const ctrl = patternInput[1];
    let matchResult;
    if (ctrl) {
        if (shift) {
            if (((pressedKey.toLocaleLowerCase() === ev.key.toLocaleLowerCase()) && ev.ctrlKey) && ev.shiftKey) {
                matchResult = 0;
            }
            else {
                matchResult = 4;
            }
        }
        else if ((pressedKey.toLocaleLowerCase() === ev.key.toLocaleLowerCase()) && ev.ctrlKey) {
            matchResult = 1;
        }
        else {
            matchResult = 4;
        }
    }
    else if (shift) {
        if ((pressedKey.toLocaleLowerCase() === ev.key.toLocaleLowerCase()) && ev.shiftKey) {
            matchResult = 2;
        }
        else {
            matchResult = 4;
        }
    }
    else if (pressedKey.toLocaleLowerCase() === ev.key.toLocaleLowerCase()) {
        matchResult = 3;
    }
    else {
        matchResult = 4;
    }
    switch (matchResult) {
        case 0: {
            handler(ev);
            break;
        }
        case 1: {
            handler(ev);
            break;
        }
        case 2: {
            handler(ev);
            break;
        }
        case 3: {
            handler(ev);
            break;
        }
        case 4: {
            break;
        }
    }
}

export function PropHelpers_createPointsFloat(coordinates) {
    return join(" ", map_1((tupledArg) => {
        const x = tupledArg[0];
        const y = tupledArg[1];
        return (x + ",") + y;
    }, coordinates));
}

export function PropHelpers_createPointsInt(coordinates) {
    return join(" ", map_1((tupledArg) => {
        const x = tupledArg[0] | 0;
        const y = tupledArg[1] | 0;
        return (x + ",") + y;
    }, coordinates));
}

export function PropHelpers_createSvgPathFloat(path) {
    return join("\n", map_1((tupledArg) => {
        const cmdType = tupledArg[0];
        const cmds = tupledArg[1];
        if (length(cmds) === 0) {
            return cmdType;
        }
        else {
            return (cmdType + " ") + join(" ", map_1((arg) => join(",", arg), cmds));
        }
    }, path));
}

export function PropHelpers_createSvgPathInt(path) {
    return join("\n", map_1((tupledArg) => {
        const cmdType = tupledArg[0];
        const cmds = tupledArg[1];
        if (length(cmds) === 0) {
            return cmdType;
        }
        else {
            return (cmdType + " ") + join(" ", map_1((arg) => join(",", arg), cmds));
        }
    }, path));
}

