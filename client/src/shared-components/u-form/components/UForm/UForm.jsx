import { useEffect, useReducer } from "react";
import { modifyChild, formInitialStateCreator, formReducer, safeSubmit, updateStateOnChange, updateStateOnBlur, callSetStates } from "./uFormLogic";

const UForm = ({children, fieldsName, fieldsValidations, setCompleteFormState, setFormState, onSubmit, ...restProps}) => {
    const [state, dispatch] = useReducer(formReducer, formInitialStateCreator(fieldsName))
    
    useEffect(() => {
        callSetStates(state, setCompleteFormState, setFormState);
    }, [])

    const onSubmitProcess = (event) => {
        event.preventDefault();
        const newState = safeSubmit(state, fieldsValidations, dispatch, onSubmit, event);
        callSetStates(newState, setCompleteFormState, setFormState);
    }

    const onChangeProcess = (event) => {
		const newState = updateStateOnChange(state, fieldsValidations, event.target.name, event.target.value, dispatch);
        callSetStates(newState, setCompleteFormState, setFormState);
    }
    
    const onBlurProcess = (event) => {
		const newState = updateStateOnBlur(state, fieldsValidations, event.target.name, dispatch);
        callSetStates(newState, setCompleteFormState, setFormState);
    }

    /* 
        map method won't work on recursive functions, and modifyChild is recursive.
        so i used forEach method.
    */
    let modifiedChildren = [];
    children.forEach((child, index) => { 
        modifiedChildren.push(modifyChild(child, index, state, onChangeProcess, onBlurProcess))
    })

    
    return (
        <form onSubmit={onSubmitProcess} {...restProps}>
            {modifiedChildren}
        </form>
    )
}

export default UForm;
