import { ToastContainer } from "react-toastify"
import CopyrightsLogo from "shared-components/copyrights-logo/CopyrightsLogo"
import ResetPasswordTextLogo from "./components/reset-password-text-logo/ResetPasswordTextLogo"
import ResetPasswordForm from "./components/reset-password-form/resetPasswordForm"

import styles from "./ResetPasswordPage.module.css"

const ResetPasswordPage = () => {
    return (
        <div className={styles.page}>
            <ToastContainer />
            <div className={styles.backLayer}>
                <div className={styles.resetPasswordContainer}>
                    <ResetPasswordTextLogo className={styles.resetPasswordLogo} />
                    <ResetPasswordForm />
                    <CopyrightsLogo className={styles.footerImg} />
                </div>
            </div>
        </div>
    )
}

export default ResetPasswordPage;
