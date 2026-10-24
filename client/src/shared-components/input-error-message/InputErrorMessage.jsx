import styles from "./inputErrorMessage.module.css"

const InputErrorMessage = ({className, message}) => {
    return <p className={`${className} ${styles.errorText}`}>{message}</p>
}

export default InputErrorMessage;
