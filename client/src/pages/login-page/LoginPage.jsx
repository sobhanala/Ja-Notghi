import JaNotghiTextLogo from "shared-components/ja-notghi-text-logo/JaNotghiTextLogo";
import LoginForm from "./components/login-form/LoginForm";
import CopyrightsLogo from "shared-components/copyrights-logo/CopyrightsLogo";
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import styles from "./LoginPage.module.css"

const LoginPage = () => {
    return (
        <div className={styles.page}>
            <ToastContainer />
            <div className={styles.backLayer}>
                <div className={styles.container}>
                    <JaNotghiTextLogo className={styles.textLogo} />
                    <LoginForm />
                    <CopyrightsLogo className={styles.footerImg}/>
                </div>
            </div>
        </div>
    )
}

export default LoginPage;
