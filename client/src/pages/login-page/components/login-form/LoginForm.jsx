import {loginUserAsync, FieldsName, FieldsValidations} from "./loginFormLogic"
import { UForm, UInput, USubmit } from "shared-components/u-form"
import { MainUrl } from "routes"

import styles from "./LoginForm.module.css"
import { Link } from "react-router-dom"

const LoginForm = () => {
    let formState = {}

    const setFormState = (state) => {
        formState = state
    }

    const onSubmit = () => {
        loginUserAsync(formState)
    }

    return (
        <UForm fieldsName={FieldsName} fieldsValidations={FieldsValidations} setFormState={setFormState} onSubmit={onSubmit} className={styles.loginForm}>
            <div className={styles.textInputGroup}>
                <label className={styles.inputLabel}>نام کاربری یا ایمیل</label>
                <UInput className={styles.textInput} type="text" name={FieldsName.identifier} 
                        placeholder="نام کاربری یا ایمیل خود را وارد کنید" />
            </div>
            <div className={styles.textInputGroup}>
                <label className={styles.inputLabel}>رمز عبور</label>
                <UInput className={styles.textInput} type="password" name={FieldsName.password} 
                        placeholder="رمز عبور خود را وارد کنید" />
            </div>
            <USubmit className={styles.formBtn} okClassName={styles.enableFormBtn} errorClassName={styles.disableFormBtn}>ورود</USubmit>
            <Link className={styles.forgotPassword} to={MainUrl.forgetPasswordPage}>فراموشی رمز عبور</Link>
            <Link className={`${styles.formBtn} ${styles.enableFormBtn}`} to={MainUrl.signUpPage}>عضویت</Link>
        </UForm>
    )
}

export default LoginForm;
