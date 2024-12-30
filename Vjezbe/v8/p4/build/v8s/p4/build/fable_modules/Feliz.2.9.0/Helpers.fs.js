import { iterate } from "../../../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/Seq.js";
import { disposeSafe } from "../../../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/Util.js";
import { toArray } from "../../../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/Option.js";

export function optDispose(disposeOption) {
    return {
        Dispose() {
            iterate((d) => {
                let copyOfStruct = d;
                disposeSafe(copyOfStruct);
            }, toArray(disposeOption));
        },
    };
}

