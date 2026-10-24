import React from "react";
import UInput from "../UInput/UInput";
import USubmit from "../USubmit/USubmit";

const enhancedOnChange = (currentOnChange, additionalOnChange) => (event) => {
    additionalOnChange(event);
    if (currentOnChange) {
        currentOnChange(event);
    }
}

const enhancedOnBlur = (currentOnBlur, additionalOnBlur) => (event) => {
    additionalOnBlur(event);
    if (currentOnBlur) {
        currentOnBlur(event);
    }
}

const clearErrorObject = (error) => {
	error.hasAny = false;
	error.message = "";
}

const getValidationInputs = (state, validation) => {
    let inputs = [];
    for (const inputName of validation.inputNames) {
        inputs.push(state[inputName]?.value);
    }
    return inputs;
}

const validate = (state, fieldsValidations) => {
    for (const fieldName in fieldsValidations) {
        const validations = fieldsValidations[fieldName];
        if (!validations) continue;
        
        let hasAnyNewError = false;
        const field = state[fieldName];
        for (const validation of validations) {
            const inputs = getValidationInputs(state, validation);
            if (!validation.validator(inputs)) {
                field.error.hasAny = true;
                field.error.message = validation.message;
                hasAnyNewError = true;
                break;
            }
        }

        if (!hasAnyNewError) {
            clearErrorObject(field.error);
        }
    }
}

const canSubmit = (state) => {
	for (let field in state) {
		if (state[field].error?.hasAny) return false;
	}
	return true;
}

const simplifyFormState = (formState) => {
    let simpleState = {};
    for (const field in formState) {
        simpleState[field] = formState[field].value;
    }
    return simpleState;
}

const isSubmitOk = (formState) => {
    if (!formState) return true;

    for (const fieldName in formState) {
        const field = formState[fieldName];
        if (field.focusOnce && field.error.hasAny) return false;
    }
    return true;
}

const getModifiedUInputElement = (UInputElement, key, formState, onChangeProcess, onBlurProcess) => {
    return React.cloneElement(UInputElement, {
        ...UInputElement.props,
        key: key,
        inputState: formState[UInputElement.props.name],
        onChange: enhancedOnChange(UInputElement.props.onChange, onChangeProcess),
        onBlur: enhancedOnBlur(UInputElement.props.onBlur, onBlurProcess),
    });
}

const getModifiedUSubmitElement = (USubmitElement, key, formState) => {
    return React.cloneElement(USubmitElement, {
        ...USubmitElement.props,
        key: key,
        isOk: isSubmitOk(formState),
    });
}

const getModifiedNestedElement = (parentElement, key, modifiedChildrenElements) => {
    return React.cloneElement(parentElement, {
        ...parentElement.props, 
        key: key,
        children: modifiedChildrenElements,
    });
}

const getModifiedElement = (element, key) => {
    return React.cloneElement(element, {
        ...element.props,
        key: key,
    });
}


export const formReducer = (state, { newState }) => {
    return newState;
}

export const modifyChild = (child, key, formState, onChangeProcess, onBlurProcess) => {
    if (child.type === UInput) {
        return getModifiedUInputElement(child, key, formState, onChangeProcess, onBlurProcess);
    } 
    else if (child.type === USubmit) {
        return getModifiedUSubmitElement(child, key, formState);
    } 
    else if (child.props && child.props.children && typeof child.props.children !== "string") {
        const childrenArray = React.Children.toArray(child.props.children);
        const modyfiedGrandChildren = childrenArray.map((grandChild, index) => modifyChild(grandChild, index, formState, onChangeProcess, onBlurProcess));
        return getModifiedNestedElement(child, key, modyfiedGrandChildren);
    } 
    else {
        return getModifiedElement(child, key);
    }
}

export const formInitialStateCreator = (inputNames) => {
    let state = {};
    for (let fieldName in inputNames) {
        state[inputNames[fieldName]] = {
            value: "",
            focusOnce: false,
            error: {
                hasAny: false,
                message: ""
            }
        }
    }

    return state;
}

export const safeSubmit = (currentState, fieldsValidations, dispatch, onSubmit, event) => {
	let tempState = {...currentState};
	for (let field in tempState) {
        tempState[field].focusOnce = true;
	}
	validate(tempState, fieldsValidations);
	dispatch({newState: tempState});
    
    if (canSubmit(tempState) && onSubmit) {
        onSubmit(event)
    }

    return tempState;
}

export const updateStateOnChange = (currentState, fieldsValidations, fieldName, newValue, dispatch) => {
    const targetField = currentState[fieldName];
	const changedField = { ...targetField, value: newValue };
	let tempState = { ...currentState, [fieldName]: changedField };
	validate(tempState, fieldsValidations);
	dispatch({ newState: tempState });
    return tempState;
}

export const updateStateOnBlur = (currentState, fieldsValidations, fieldName, dispatch) => {
	const targetField = currentState[fieldName];
	const changedField = { ...targetField, focusOnce: true };
	let tempState = { ...currentState, [fieldName]: changedField };
	validate(tempState, fieldsValidations);
	dispatch({ newState: tempState });
    return tempState;
}

export const callSetStates = (formState, setCompleteFormState, setFormState) => {
    if (setCompleteFormState) {
        setCompleteFormState({...formState});
    }
    if (setFormState) {
        setFormState(simplifyFormState({...formState}));
    }
}
