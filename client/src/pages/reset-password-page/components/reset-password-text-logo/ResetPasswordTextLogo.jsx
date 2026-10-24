import styles from "./ResetPasswordTextLogo.module.css"
import logo from "assets/reset-password-logo/logo.svg"

const ResetPasswordTextLogo = ({className, style}) => {
    return (
        <div className={className} style={style}>
            <img className={styles.textLogoImg} src={logo} alt="text-logo" />
        </div>
    )
}

export default ResetPasswordTextLogo;
