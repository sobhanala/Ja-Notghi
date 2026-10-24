import TextLogo from "app/components/text-logo/TextLogo"
import NavBarBtn, {NavBarBtnType} from "app/components/nav-bar-btn/NavBarBtn"

import styles from "./NavigationBar.module.css"

const NavigationBar = ({...props}) => {
    return (
        <div {...props}>
            <div className={styles.navigationBar}>
                <TextLogo className={styles.textLogo}/>
                <div className={styles.scrollableContainer}>
                    <nav className={styles.menuContainer}>
                        <NavBarBtn type={NavBarBtnType.MAIN_PAGE} className={styles.navBtn} />
                        <NavBarBtn type={NavBarBtnType.PROFILE} className={styles.navBtn} />
                        <NavBarBtn type={NavBarBtnType.SEARCH} className={styles.navBtn} />
                        <NavBarBtn type={NavBarBtnType.SETTING} className={styles.navBtn} />
                        <NavBarBtn type={NavBarBtnType.BOOKMARK} className={styles.navBtn} />
                        <NavBarBtn type={NavBarBtnType.INBOX} className={styles.navBtn} />
                        <NavBarBtn type={NavBarBtnType.INFO} className={styles.navBtn} />
                    </nav>
                </div>
                <nav className={styles.contactContainer}>
                    <i className={`fa-brands fa-linkedin ${styles.contactIcon}`}></i>
                    <i className={`fa-brands fa-telegram ${styles.contactIcon}`}></i>
                    <i className={`fa-brands fa-youtube ${styles.contactIcon}`}></i>
                    <i className={`fa-brands fa-square-instagram ${styles.contactIcon}`}></i>
                </nav>
            </div>
        </div>
    )
}

export default NavigationBar
