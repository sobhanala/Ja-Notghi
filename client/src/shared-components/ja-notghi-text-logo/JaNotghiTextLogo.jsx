import styles from "./JaNotghiTextLogo.module.css"
import logo from "assets/ja-notghi-text-logo/logo.svg"

const JaNotghiTextLogo = ({className, style}) => {
    return (
        <div className={className} style={style}>
            <img className={styles.textLogoImg} src={logo} alt="text-logo" />
        </div>
    )
}

export default JaNotghiTextLogo;
