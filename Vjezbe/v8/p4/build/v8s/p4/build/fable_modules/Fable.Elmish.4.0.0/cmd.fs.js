import { singleton, concat, map, empty, iterate } from "../../../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/List.js";
import { singleton as singleton_1 } from "../../../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/AsyncBuilder.js";
import { startImmediate, catchAsync } from "../../../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/Async.js";
import { Timer_delay } from "./prelude.fs.js";

/**
 * Execute the commands using the supplied dispatcher
 */
export function Cmd_exec(onError, dispatch, cmd) {
    iterate((call) => {
        try {
            call(dispatch);
        }
        catch (ex) {
            onError(ex);
        }
    }, cmd);
}

/**
 * None - no commands, also known as `[]`
 */
export function Cmd_none() {
    return empty();
}

/**
 * When emitting the message, map to another type
 */
export function Cmd_map(f, cmd) {
    return map((g) => ((arg_1) => {
        g((arg) => {
            arg_1(f(arg));
        });
    }), cmd);
}

/**
 * Aggregate multiple commands
 */
export function Cmd_batch(cmds) {
    return concat(cmds);
}

/**
 * Command to call the effect
 */
export function Cmd_ofEffect(effect) {
    return singleton(effect);
}

/**
 * Command to evaluate a simple function and map the result
 * into success or error (of exception)
 */
export function Cmd_OfFunc_either(task, arg, ofSuccess, ofError) {
    const bind = (dispatch) => {
        try {
            return dispatch(ofSuccess(task(arg)));
        }
        catch (x) {
            return dispatch(ofError(x));
        }
    };
    return singleton(bind);
}

/**
 * Command to evaluate a simple function and map the success to a message
 * discarding any possible error
 */
export function Cmd_OfFunc_perform(task, arg, ofSuccess) {
    const bind = (dispatch) => {
        try {
            dispatch(ofSuccess(task(arg)));
        }
        catch (x) {
        }
    };
    return singleton(bind);
}

/**
 * Command to evaluate a simple function and map the error (in case of exception)
 */
export function Cmd_OfFunc_attempt(task, arg, ofError) {
    const bind = (dispatch) => {
        try {
            task(arg);
        }
        catch (x) {
            dispatch(ofError(x));
        }
    };
    return singleton(bind);
}

/**
 * Command that will evaluate an async block and map the result
 * into success or error (of exception)
 */
export function Cmd_OfAsyncWith_either(start, task, arg, ofSuccess, ofError) {
    const bind = (dispatch) => singleton_1.Delay(() => singleton_1.Bind(catchAsync(task(arg)), (_arg) => {
        let x_1, x;
        const r = _arg;
        dispatch((r.tag === 1) ? ((x_1 = r.fields[0], ofError(x_1))) : ((x = r.fields[0], ofSuccess(x))));
        return singleton_1.Zero();
    }));
    return singleton((arg_1) => {
        start(bind(arg_1));
    });
}

/**
 * Command that will evaluate an async block and map the success
 */
export function Cmd_OfAsyncWith_perform(start, task, arg, ofSuccess) {
    const bind = (dispatch) => singleton_1.Delay(() => singleton_1.Bind(catchAsync(task(arg)), (_arg) => {
        const r = _arg;
        if (r.tag === 0) {
            const x = r.fields[0];
            dispatch(ofSuccess(x));
            return singleton_1.Zero();
        }
        else {
            return singleton_1.Zero();
        }
    }));
    return singleton((arg_1) => {
        start(bind(arg_1));
    });
}

/**
 * Command that will evaluate an async block and map the error (of exception)
 */
export function Cmd_OfAsyncWith_attempt(start, task, arg, ofError) {
    const bind = (dispatch) => singleton_1.Delay(() => singleton_1.Bind(catchAsync(task(arg)), (_arg) => {
        const r = _arg;
        if (r.tag === 1) {
            const x = r.fields[0];
            dispatch(ofError(x));
            return singleton_1.Zero();
        }
        else {
            return singleton_1.Zero();
        }
    }));
    return singleton((arg_1) => {
        start(bind(arg_1));
    });
}

export function Cmd_OfAsync_start(x) {
    Timer_delay(1, (_arg) => {
        startImmediate(x);
    });
}

/**
 * Command to call `promise` block and map the results
 */
export function Cmd_OfPromise_either(task, arg, ofSuccess, ofError) {
    const bind = (dispatch) => {
        task(arg).then((arg_3) => dispatch(ofSuccess(arg_3))).catch((arg_2) => dispatch(ofError(arg_2)));
    };
    return singleton(bind);
}

/**
 * Command to call `promise` block and map the success
 */
export function Cmd_OfPromise_perform(task, arg, ofSuccess) {
    const bind = (dispatch) => {
        task(arg).then((arg_1) => dispatch(ofSuccess(arg_1)));
    };
    return singleton(bind);
}

/**
 * Command to call `promise` block and map the error
 */
export function Cmd_OfPromise_attempt(task, arg, ofError) {
    const bind = (dispatch) => {
        task(arg).catch((arg_2) => {
            dispatch(ofError(arg_2));
        });
    };
    return singleton(bind);
}

