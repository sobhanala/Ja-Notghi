import { sendPasswordResetAsync, FieldsName, FieldsValidations } from "./forgetPasswordFormLogic"
import { UForm, UInput, USubmit } from "shared-components/u-form"

import styles from "./forgetPassordForm.module.css"

const ForgetPasswordForm = () => {
    let formState = {}

    const onSubmit = () => {
        sendPasswordResetAsync(formState[FieldsName.email])
    }

    const setFormState = (state) => {
        formState = state
    }

    return (
        <UForm fieldsName={FieldsName} fieldsValidations={FieldsValidations} setFormState={setFormState} 
               className={styles.forgetPasswordForm} onSubmit={onSubmit}>
            <div className={styles.textInputGroup}>
                <label className={styles.inputLabel}>ایمیل</label>
                <UInput className={styles.textInput} messageClassName={styles.inputErrorMessage} type="text" 
                        name={FieldsName.email} placeholder="ایمیل خود را وارد کنید." />
            </div>
            <USubmit className={styles.formBtn} okClassName={styles.enableFormBtn} errorClassName={styles.disableFormBtn}>ارسال لینک</USubmit>
        </UForm>
    )
}

export default ForgetPasswordForm
