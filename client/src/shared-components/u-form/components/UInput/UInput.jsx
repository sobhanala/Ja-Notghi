import styles from './UInput.module.css'

const UInput = ({messageClassName, inputState, showErrorMessage=true, ...restProps}) => {
    return (
        <>
        <input {...restProps} />
        {showErrorMessage && inputState?.focusOnce && inputState.error?.hasAny && 
            <p className={`${styles.UInputErrorMessage} ${messageClassName}`}>{inputState.error.message}</p>}
        </>
    )
}

export default UInput;
