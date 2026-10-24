import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMagnifyingGlass, faBars } from '@fortawesome/free-solid-svg-icons';
import CopyrightsLogo from "shared-components/copyrights-logo/CopyrightsLogo"

import styles from "./TimelineSideContent.module.css"

const TimelineSideContent = () => {
    return (
        <div className={styles.sideContent}>
            <div className={styles.searchBox}>
                <FontAwesomeIcon icon={faMagnifyingGlass} className={styles.searchBoxIcon} tabIndex="0" />
                <input className={styles.searchBoxInput} type="text" placeholder="جست و جو" />
                <FontAwesomeIcon icon={faBars} className={styles.searchBoxIcon} tabIndex="0" />
            </div>
            <div className={styles.messageBox}>
                <div className={styles.messageBoxHeader}>
                    <div className={`${styles.profileImageContainer} ${styles.HeaderImage}`}>
                        ر
                    </div>
                    <div className={`${styles.profileImageContainer} ${styles.HeaderImage}`}>
                        ر
                    </div>
                    <div className={`${styles.profileImageContainer} ${styles.HeaderImage}`}>
                        ر
                    </div>
                    <div className={`${styles.profileImageContainer} ${styles.HeaderImage}`}>
                        ر
                    </div>
                    <div className={styles.searchIcon}>
                        <FontAwesomeIcon icon={faMagnifyingGlass} />
                    </div>
                </div>
                <div className={styles.messageContainer}>
                    <div className={styles.message}>
                        <div className={styles.messageProfileImage}>
                            ر
                        </div>
                        <div className={styles.messageContent}>
                            <p className={styles.messageContentHeader}>رضا مستعان</p>
                            <p className={styles.messageContentText}>سلام و درود، یک برنامه ای بچینیم هم رو ببینیم</p>
                        </div>
                        <div className={styles.messageDetail}>
                            <p className={styles.messageTime}>14:12</p>
                            <div className={styles.messageNotification}>
                                1
                            </div>
                        </div>
                    </div>
                    <div className={styles.message}>
                        <div className={styles.messageProfileImage}>
                            ر
                        </div>
                        <div className={styles.messageContent}>
                            <p className={styles.messageContentHeader}>رضا مستعان</p>
                            <p className={styles.messageContentText}>سلام این یک متن طولانی است که قرار است بیش از اندازه ای پدر مثوم باشد</p>
                        </div>
                        <div className={styles.messageDetail}>
                            <p className={styles.messageTime}>14:12</p>
                            <div className={styles.messageNotification}>
                                1
                            </div>
                        </div>
                    </div>
                    <div className={styles.message}>
                        <div className={styles.messageProfileImage}>
                            ر
                        </div>
                        <div className={styles.messageContent}>
                            <p className={styles.messageContentHeader}>محمد جواد ابراهیمی ترکمان اصل</p>
                            <p className={styles.messageContentText}>سلام و درود، یک برنامه ای بچینیم هم رو ببینیم</p>
                        </div>
                        <div className={styles.messageDetail}>
                            <p className={styles.messageTime}>14:12</p>
                            <div className={styles.messageNotification}>
                                1
                            </div>
                        </div>
                    </div>
                    <div className={styles.message}>
                        <div className={styles.messageProfileImage}>
                            ر
                        </div>
                        <div className={styles.messageContent}>
                            <p className={styles.messageContentHeader}>رضا مستعان</p>
                            <p className={styles.messageContentText}>بد جوری میخوامت عزیزم یمدونم ایت رو کسی قرار نیست ببینیه اوممم اوففف</p>
                        </div>
                        <div className={styles.messageDetail}>
                            <p className={styles.messageTime}>14:12</p>
                            <div className={styles.messageNotification}>
                                1
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <CopyrightsLogo className={styles.copyrightsLogo}/>
        </div>
    )
}

export default TimelineSideContent
