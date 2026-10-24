import SignupForm from "./components/signup-form/SignupForm"
import CopyrightsLogo from "shared-components/copyrights-logo/CopyrightsLogo"
import SignupTextLogo from "./components/signup-text-logo/SingupTextLogo"

import { ToastContainer } from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css'

import styles from "./SignUpPage.module.css"

const SignUpPage = () => {
    return (
        <div className={styles.page}>
            <ToastContainer />
            <div className={styles.backLayer}>
                <div className={styles.signupContainer}>
                    <SignupTextLogo className={styles.signupLogo}/>
                    <SignupForm className={styles.formContainer}/>
                    <CopyrightsLogo className={styles.footerImg}/>
                </div>
            </div>
        </div>
    )
}

export default SignUpPage;
