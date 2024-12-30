import { parse } from "../../../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/Int32.js";
import * as react from "react";
import * as client from "react-dom/client";
import { ProgramModule_withSetState, ProgramModule_view } from "../Fable.Elmish.4.0.0/program.fs.js";
import * as react_dom from "react-dom";
import { uncurry2 } from "../../../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/Util.js";
import { Common_lazyView2With } from "./common.fs.js";

export const Program_Internal_useRootApi = (() => {
    try {
        return parse(react.version.slice(undefined, 1 + 1), 511, false, 32) >= 18;
    }
    catch (matchValue) {
        return false;
    }
})();

export function Program_Internal_withReactBatchedUsing(lazyView2With, placeholderId, program) {
    let setState;
    let lastRequest = undefined;
    if (Program_Internal_useRootApi) {
        const root = client.createRoot(document.getElementById(placeholderId));
        setState = ((model) => ((dispatch) => {
            if (lastRequest != null) {
                const r = lastRequest;
                window.cancelAnimationFrame(r);
            }
            lastRequest = window.requestAnimationFrame((_arg) => {
                root.render(lazyView2With((x) => ((y) => (x === y)), ProgramModule_view(program), model, dispatch));
            });
        }));
    }
    else {
        setState = ((model_1) => ((dispatch_1) => {
            if (lastRequest != null) {
                const r_1 = lastRequest;
                window.cancelAnimationFrame(r_1);
            }
            lastRequest = window.requestAnimationFrame((_arg_1) => {
                react_dom.render(lazyView2With((x_1) => ((y_1) => (x_1 === y_1)), ProgramModule_view(program), model_1, dispatch_1), document.getElementById(placeholderId));
            });
        }));
    }
    return ProgramModule_withSetState(uncurry2(setState), program);
}

export function Program_Internal_withReactSynchronousUsing(lazyView2With, placeholderId, program) {
    let setState;
    if (Program_Internal_useRootApi) {
        const root = client.createRoot(document.getElementById(placeholderId));
        setState = ((model) => ((dispatch) => {
            root.render(lazyView2With((x) => ((y) => (x === y)), ProgramModule_view(program), model, dispatch));
        }));
    }
    else {
        setState = ((model_1) => ((dispatch_1) => {
            react_dom.render(lazyView2With((x_1) => ((y_1) => (x_1 === y_1)), ProgramModule_view(program), model_1, dispatch_1), document.getElementById(placeholderId));
        }));
    }
    return ProgramModule_withSetState(uncurry2(setState), program);
}

export function Program_Internal_withReactHydrateUsing(lazyView2With, placeholderId, program) {
    let setState;
    if (Program_Internal_useRootApi) {
        let root = undefined;
        setState = ((model) => ((dispatch) => {
            if (root != null) {
                const root_1 = root;
                root_1.render(lazyView2With((x_1) => ((y_1) => (x_1 === y_1)), ProgramModule_view(program), model, dispatch));
            }
            else {
                root = client.hydrateRoot(document.getElementById(placeholderId), lazyView2With((x) => ((y) => (x === y)), ProgramModule_view(program), model, dispatch));
            }
        }));
    }
    else {
        setState = ((model_1) => ((dispatch_1) => {
            react_dom.hydrate(lazyView2With((x_2) => ((y_2) => (x_2 === y_2)), ProgramModule_view(program), model_1, dispatch_1), document.getElementById(placeholderId));
        }));
    }
    return ProgramModule_withSetState(uncurry2(setState), program);
}

/**
 * Renders React root component inside html element identified by placeholderId.
 * Uses `requestAnimationFrame` to batch updates to prevent drops in frame rate.
 * NOTE: This may have unexpected effects in React controlled inputs, see https://github.com/elmish/react/issues/12
 */
export function Program_withReactBatched(placeholderId, program) {
    return Program_Internal_withReactBatchedUsing((equal, view, state, dispatch) => Common_lazyView2With(uncurry2(equal), uncurry2(view), state, dispatch), placeholderId, program);
}

/**
 * Renders React root component inside html element identified by placeholderId.
 * New renders are triggered immediately after an update.
 */
export function Program_withReactSynchronous(placeholderId, program) {
    return Program_Internal_withReactSynchronousUsing((equal, view, state, dispatch) => Common_lazyView2With(uncurry2(equal), uncurry2(view), state, dispatch), placeholderId, program);
}

/**
 * Renders React root component inside html element identified by placeholderId using `React.hydrate`.
 */
export function Program_withReactHydrate(placeholderId, program) {
    return Program_Internal_withReactHydrateUsing((equal, view, state, dispatch) => Common_lazyView2With(uncurry2(equal), uncurry2(view), state, dispatch), placeholderId, program);
}

