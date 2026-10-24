import logo from "assets/signup-page/signup-text-logo.svg"
import styles from './SignupTextLogo.module.css'

const SignupTextLogo = ({className}) => {
    return (
        <div className={className}>
            <img className={styles.imgLogo} src={logo} alt="Signup Logo" />
        </div>
    )
}

export default SignupTextLogo
