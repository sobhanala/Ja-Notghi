import styles from "./forgetPasswordTextLogo.module.css"
import logo from "assets/forget-password-logo/logo.svg"

const ForgetPasswordTextLogo = ({className, style}) => {
    return (
        <div className={className} style={style}>
            <img className={styles.textLogoImg} src={logo} alt="text-logo" />
        </div>
    )
}

export default ForgetPasswordTextLogo;