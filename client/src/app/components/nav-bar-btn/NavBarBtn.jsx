import { NavBarBtnType, NavBarBtnContent } from "./navBarBtnLogic";
import styles from "./NavBarBtn.module.css"

const NavBarBtn = ({type, className, ...restProps}) => {
    return (
        <button className={`${className}  ${styles.navBarBtn}`} {...restProps}>
            <i className={`${NavBarBtnContent[type].icon} ${styles.navBarBtnIcon}`}/>
            <p className={styles.navBarBtnText}>{NavBarBtnContent[type].text}</p>
        </button>
    )
}

export default NavBarBtn;
export { NavBarBtnType };
