import defaultImage from "assets/app/components/profile-image/default-profile-image.png"

import styles from "./ProfileImage.module.css"

const ProfileImage = ({className, imageAddress, ...restProps}) => {
    return (
        <div className={`${styles.profileImageContainer} ${className}`} {...restProps}>
            <img className={styles.profileImage} src={imageAddress ? imageAddress : defaultImage} alt="default-profile-image"></img>
        </div>
    )
}

export default ProfileImage;
