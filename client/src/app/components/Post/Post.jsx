import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faComment, faEllipsisVertical, faThumbsDown, faThumbsUp } from '@fortawesome/free-solid-svg-icons';

import styles from "./Post.module.css"

const Post = ({className, data, ...restProps}) => {
    return (
        <div className={`${styles.post} ${className}`} {...restProps}>
            <div className={styles.postHeader}>
                <div className={`${styles.profileImageContainer} ${styles.postHeaderImage}`}>
                    {data.profile}
                </div>
                <div className={styles.postHeaderTag}>
                    {data.username}
                </div>
                <FontAwesomeIcon icon={faEllipsisVertical} className={styles.postHeaderEllipsis} />
            </div>
            <p className={styles.postContent}>
                {data.content}
            </p>
            <div className={styles.postFooter}>
                <span className={styles.postFooterIconCounter}>{data.numOfComments}</span>
                <FontAwesomeIcon icon={faComment} className={styles.postFooterIcon}/>
                <span className={styles.postFooterIconCounter}>{data.numOfDislikes}</span>
                <FontAwesomeIcon icon={faThumbsDown} className={styles.postFooterIcon}/>
                <span className={styles.postFooterIconCounter}>{data.numOfLikes}</span>
                <FontAwesomeIcon icon={faThumbsUp} className={styles.postFooterIcon}/>
            </div>
        </div>
    )
}

export default Post
