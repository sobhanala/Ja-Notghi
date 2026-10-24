import styles from "./CopyrightsLogo.module.css"
import logo from "assets/copyrights-logo/logo.svg"

const CopyrightsLogo = ({className, style}) => {
    return (
        <div className={className} style={style}>
            <img className={styles.LogoImg} src={logo} alt="text-logo" />
        </div>
    )
}

export default CopyrightsLogo;
