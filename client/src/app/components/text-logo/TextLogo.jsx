import logo from "assets/app/components/text-logo/text-logo.svg"
import styles from "./TextLogo.module.css"

const TextLogo = ({...restProps}) => {
    return (
        <div {...restProps}>
            <img className={styles.logo} src={logo} alt="text-logo" />
        </div>
    )
}

export default TextLogo;
