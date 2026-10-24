import NavigationBar from 'app/layouts/navigation-bar/NavigationBar';
import NavigationFooter from '../navigation-footer/NavigationFooter';
import MainHeader from 'app/layouts/main-header/MainHeader';
import ContentNotFoundMainContent from 'app/content-not-found/main-content/ContentNotFoundMainContent';

import styles from './ContentNotFoundLayout.module.css';

const ContentNotFoundLayout = () => {
    return (
        <div className={styles.app}>
            <MainHeader className={styles.mainHeader}/>
            <NavigationBar className={styles.navigationBar}/>
            
            <div className={styles.mainPart}>
                <div className={styles.mainContent}>
                    <ContentNotFoundMainContent />
                </div>
            </div>

            <NavigationFooter className={styles.navigationFooter} />
        </div>
    )
}

export default ContentNotFoundLayout
