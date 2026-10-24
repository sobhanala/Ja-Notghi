import { Routes, Route } from "react-router-dom"

import NavigationBar from 'app/layouts/navigation-bar/NavigationBar';
import NavigationFooter from "../navigation-footer/NavigationFooter";
import MainHeader from 'app/layouts/main-header/MainHeader';
import TimelineMaincontent from 'app/timeline/main-content/TimelineMainContent';
import TimelineSideContent from 'app/timeline/side-content/TimelineSideContent';
import { AppUrl } from "routes";

import styles from './DefaultAppLayout.module.css';

const DefaultAppLayout = () => {
    return (
        <div className={styles.app}>
            <MainHeader className={styles.mainHeader}/>
            <NavigationBar className={styles.navigationBar}/>
            
            <div className={styles.mainPart}>
                <MainHeader className={styles.bigMainHeader}/>
                <div className={styles.mainContent}>
                    <Routes>
                        <Route path={AppUrl.timeline} element={<TimelineMaincontent />} />
                    </Routes>
                </div>
            </div>
            
            <div className={styles.sideContent}>
                <Routes>
                    <Route path={AppUrl.timeline} element={<TimelineSideContent />} />
                </Routes>
            </div>

            <NavigationFooter className={styles.navigationFooter} />
        </div>
    )
}

export default DefaultAppLayout
