import { resetPasswordAsync, FieldsName, FieldsValidations } from "./resetPasswordFormLogic"
import { useLocation } from "react-router-dom"
import { UForm, UInput, USubmit } from "shared-components/u-form"

import styles from "./resetPasswordForm.module.css"
import { useEffect } from "react"

const ResetPasswordForm = () => {
    let formState = {}
    let email = ""
    let token = ""
    const location = useLocation()
    
    useEffect(() => {
        const queryParams = new URLSearchParams(location.search)
        email = queryParams.get("email")
        token = queryParams.get("token")
    }, [])

    const onSubmit = () => {
        resetPasswordAsync(email, token, formState[FieldsName.newPassword])
    }

    const setFormState = (state) => {
        formState = state
    }

    return (
        <UForm fieldsName={FieldsName} fieldsValidations={FieldsValidations} setFormState={setFormState} 
               className={styles.resetPasswordForm} onSubmit={onSubmit}>
            <div className={styles.textInputGroup}>
                <label className={styles.inputLabel}>رمز عبور جدید</label>
                <UInput className={styles.textInput} messageClassName={styles.inputErrorMessage} type="password" 
                        name={FieldsName.newPassword} placeholder="رمز عبور جدید خود را وارد کنید" />
            </div>
            <div className={styles.textInputGroup}>
                <label className={styles.inputLabel}>تکرار رمز عبور جدید</label>
                <UInput className={styles.textInput} messageClassName={styles.inputErrorMessage} type="password" 
                        name={FieldsName.confirmNewPassword} placeholder="رمز عبور خود را تکرار کنید" />
            </div>
            <USubmit className={styles.formBtn} okClassName={styles.enableFormBtn} errorClassName={styles.disableFormBtn}>تغییر رمز عبور</USubmit>
        </UForm>
    )
}

export default ResetPasswordForm
