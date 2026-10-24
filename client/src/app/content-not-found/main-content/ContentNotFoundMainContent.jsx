import { Link } from "react-router-dom";

import boyImg from "assets/content-not-found/boy-image.png"
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faHouse } from '@fortawesome/free-solid-svg-icons';
import { AppUrl } from "routes";

import styles from "./ContentNotFoundMainContent.module.css"

const ContentNotFoundMainContent = () => {
    return (
        <>
            <img className={styles.img} src={boyImg} alt="boy-img"/>
            <p className={styles.text}>صفحه درخواستی شما وجود ندارد.<br />
            لطفا لینک URL خودتان را چک کنید و یا به صفحه اصلی برگردید.</p>
            <Link className={styles.link} to={AppUrl.prefixUrl + AppUrl.timeline}>
                <FontAwesomeIcon icon={faHouse} className={styles.linkIcon} />
                    صفحه اصلی
            </Link>
        </>
    )
}

export default ContentNotFoundMainContent
