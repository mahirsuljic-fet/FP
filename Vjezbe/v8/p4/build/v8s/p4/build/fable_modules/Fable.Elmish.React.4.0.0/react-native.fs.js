import { FSharpRef, Record } from "../../../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/Types.js";
import { obj_type, record_type, lambda_type, class_type, unit_type } from "../../../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/Reflection.js";
import { defaultOf, createAtom } from "../../../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/Util.js";
import { value as value_1 } from "../../../../../../../../v8s/p4/build/fable_modules/fable-library-js.4.17.0/Option.js";
import { Component } from "react";
import { AppRegistry } from "react-native";
import { ProgramModule_withSetState, ProgramModule_view } from "../Fable.Elmish.4.0.0/program.fs.js";

export class Components_AppState extends Record {
    constructor(render, setState) {
        super();
        this.render = render;
        this.setState = setState;
    }
}

export function Components_AppState_$reflection() {
    return record_type("Elmish.ReactNative.Components.AppState", [], Components_AppState, () => [["render", lambda_type(unit_type, class_type("Fable.React.ReactElement"))], ["setState", lambda_type(Components_AppState_$reflection(), unit_type)]]);
}

export let Components_appState = createAtom(undefined);

export class Components_App extends Component {
    constructor(props) {
        super(props);
        let objectArg;
        const this$ = new FSharpRef(defaultOf());
        this$.contents = this;
        this["init@15"] = 1;
        if (Components_appState() != null) {
            const state = Components_appState();
            Components_appState(new Components_AppState(state.render, (objectArg = this$.contents, (value) => {
                this.state = value;
            })));
            this.state = state;
        }
        else {
            throw new Error("was Elmish.ReactNative.Program.withReactNative called?");
        }
    }
    componentDidMount() {
        const this$ = this;
        Components_appState(new Components_AppState(value_1(Components_appState()).render, (s) => {
            this$.setState((_arg, _arg_1) => s);
        }));
    }
    componentWillUnmount() {
        let bind$0040;
        const this$ = this;
        Components_appState((bind$0040 = value_1(Components_appState()), new Components_AppState((this$.state).render, (value) => {
        })));
    }
    render() {
        const this$ = this;
        return (this$.state).render();
    }
}

export function Components_App_$reflection() {
    return class_type("Elmish.ReactNative.Components.App", undefined, Components_App, class_type("Fable.React.Component`2", [obj_type, Components_AppState_$reflection()], Component));
}

export function Components_App_$ctor_4E60E31B(props) {
    return new Components_App(props);
}

/**
 * Setup rendering of root ReactNative component
 */
export function Program_withReactNative(appKey, program) {
    AppRegistry.registerComponent(appKey, () => Components_App);
    const setState = (m, d) => {
        if (Components_appState() != null) {
            const state = Components_appState();
            state.setState(new Components_AppState(() => ProgramModule_view(program)(m)(d), state.setState));
        }
        else {
            Components_appState(new Components_AppState(() => ProgramModule_view(program)(m)(d), (value) => {
            }));
        }
    };
    return ProgramModule_withSetState(setState, program);
}

