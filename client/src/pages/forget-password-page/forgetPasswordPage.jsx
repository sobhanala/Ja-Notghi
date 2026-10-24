import ForgetPasswordTextLogo from "./components/forget-password-text-logo/forgetPasswordTextLogo"
import ForgetPasswordForm from "./components/forget-password-form/forgetPasswordForm"
import CopyrightsLogo from "shared-components/copyrights-logo/CopyrightsLogo"
import { ToastContainer } from "react-toastify"

import styles from "./forgetPasswprodPage.module.css"

const ForgetPasswordPage = () => {
    return (
        <div className={styles.page}>
            <ToastContainer />
            <div className={styles.backLayer}>
                <div className={styles.forgotPasswordContainer}>
                    <ForgetPasswordTextLogo className={styles.forgotPasswordLogo} />
                    <ForgetPasswordForm />
                    <CopyrightsLogo className={styles.footerImg}/>
                </div>
            </div>
        </div>
    )
}

export default ForgetPasswordPage
