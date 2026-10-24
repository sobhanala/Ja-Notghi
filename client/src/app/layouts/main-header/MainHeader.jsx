import styles from "./MainHeader.module.css"
import TextLogo from "app/components/text-logo/TextLogo";
import ProfileImage from "app/components/profile-image/ProfileImage";

const MainHeader = ({...props}) => {
    return (
        <div {...props}>
            <div className={styles.headerContainer}>
                <p className={styles.contentNameText}>صفحه اصلی</p>
                <i className={`fa-solid fa-ellipsis-vertical ${styles.menuIcon}`}></i>
                <TextLogo className={styles.textLogo}/>
                <ProfileImage className={styles.userProfileImage} />
            </div>
        </div>
    )
} 

export default MainHeader;
