import NavBarBtn, { NavBarBtnType } from "app/components/nav-bar-btn/NavBarBtn"

import styles from "./NavigationFooter.module.css"

const NavigationFooter = ({...props}) => {
    return (
        <div {...props}>
            <div className={styles.navigationFooter}>
                <NavBarBtn type={NavBarBtnType.INBOX} className={styles.navBtn} />
                <NavBarBtn type={NavBarBtnType.SETTING} className={styles.navBtn} />
                <NavBarBtn type={NavBarBtnType.PROFILE} className={styles.navBtn} />
                <NavBarBtn type={NavBarBtnType.MAIN_PAGE} className={styles.navBtn} />
            </div>
        </div>
    )
}

export default NavigationFooter
