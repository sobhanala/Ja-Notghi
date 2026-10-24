import { Link } from "react-router-dom";
import girlImg from "assets/page-not-found/girl-image.png"
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faRightToBracket } from '@fortawesome/free-solid-svg-icons';
import { MainUrl } from "routes";

import styles from "./PageNotFound.module.css"

const PageNotFound = () => {
    return (
        <div className={styles.page}>
            <img className={styles.pageImg} src={girlImg} alt="girl-img"/>
            <p className={styles.pageText}>صفحه موردنظر شما پیدا نشد، سعی کنید به صفحه قبل بازگردید.<br />
            برای دسترسی بهتر به برنامه ابتدا وارد شوید.</p>
            <Link className={styles.pageLink} to={MainUrl.loginPage}>
                <FontAwesomeIcon icon={faRightToBracket} className={styles.pageLinkIcon} />
                    ورود
            </Link>
        </div>
    )
}

export default PageNotFound;
