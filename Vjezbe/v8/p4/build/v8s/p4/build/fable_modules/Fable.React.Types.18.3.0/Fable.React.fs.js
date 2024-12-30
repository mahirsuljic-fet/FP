import { Record } from "../../../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/Types.js";
import { record_type, string_type } from "../../../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/Reflection.js";

export class FragmentProps extends Record {
    constructor(key) {
        super();
        this.key = key;
    }
}

export function FragmentProps_$reflection() {
    return record_type("Fable.React.FragmentProps", [], FragmentProps, () => [["key", string_type]]);
}

