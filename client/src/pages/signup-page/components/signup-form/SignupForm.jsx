import { DatePicker } from "zaman"
import { Link } from "react-router-dom"
import { useNavigate } from "react-router-dom"
import { UForm, UInput, USubmit } from "shared-components/u-form"

import { MainUrl } from "routes"
import Genders from "const/genders"

import {signupUserAsync, formatDate, FieldsName, FieldsValidations} from "./signupFormLogic"
import styles from "./SignUpForm.module.css"

const SignupForm = ({className}) => {
    let formState = {}
    let date = new Date()
    let gender = 0
    const navigate = useNavigate()

    const setFormState = (state) => {
        formState = state
    }

    const handleDateChange = (event) => {
        date = formatDate(new Date(event.value))
    }

    const handleGenderChange = (event) => {
        gender = +event.target.value
    }

    const handleSubmit = () => {
        formState[FieldsName.birthDate] = date
        formState[FieldsName.gender] = gender
        signupUserAsync(formState, navigate)
    }

    return (
        <div className={className}>
            <UForm fieldsName={FieldsName} fieldsValidations={FieldsValidations} setFormState={setFormState} onSubmit={handleSubmit} 
                   className={styles.signupForm}>
                <div className={styles.formInputColumn}>
                    <div className={styles.textInputGroup}>
                        <label className={styles.inputLabel}>نام</label>
                        <UInput messageClassName={styles.inputErrorMessage} className={styles.textInput} type="text" 
                                name={FieldsName.firstName} placeholder="نام" />
                    </div>
                    <div className={styles.textInputGroup}>
                        <label className={styles.inputLabel}>نام خانوادگی</label>
                        <UInput messageClassName={styles.inputErrorMessage} className={styles.textInput} type="text" 
                                name={FieldsName.lastName} placeholder="نام خانوادگی" />
                    </div>
                    <div className={styles.textInputGroup}>
                        <label className={styles.inputLabel}>تاریخ تولد</label>
                        <DatePicker inputClass={styles.textInput}
                                    className={styles.datePicker}
                                    inputAttributes= {{placeholder: "تاریخ تولد"}}
                                    onChange={handleDateChange}
                                    dateFormat="YYYY MMMM DD"/>
                    </div>
                    <div className={styles.textInputGroup}>
                        <label className={styles.inputLabel}>جنسیت</label>
                        <select className={styles.textInput} name={FieldsName.gender} onChange={handleGenderChange} >
                            <option value="" disabled>جنسیت</option>
                            <option value={Genders.male}>مرد</option>
                            <option value={Genders.female}>زن</option>
                            <option value={Genders.nonBinary}>دیگر</option>
                            <option value={Genders.notImportant}>علاقه مند به گفتن نیست</option>
                        </select>
                    </div>
                </div>
                <div className={styles.formInputColumn}>
                    <div className={styles.textInputGroup}>
                        <label className={styles.inputLabel}>ایمیل</label>
                        <UInput messageClassName={styles.inputErrorMessage} className={`${styles.textInput} ${styles.engTextInput}`} 
                                type="email" name={FieldsName.email} placeholder="username@example.com" />
                    </div>
                    <div className={styles.textInputGroup}>
                        <label className={styles.inputLabel}>نام کاربری</label>
                        <UInput messageClassName={styles.inputErrorMessage} className={`${styles.textInput} ${styles.engTextInput}`} 
                                type="text" name={FieldsName.userName} placeholder="username" />
                    </div>
                    <div className={styles.textInputGroup}>
                        <label className={styles.inputLabel}>رمز ورود</label>
                        <UInput messageClassName={styles.inputErrorMessage} className={`${styles.textInput} ${styles.engTextInput}`} 
                                type="password" name={FieldsName.password} placeholder="password" />
                    </div>
                    <div className={styles.textInputGroup}>
                        <label className={styles.inputLabel}>تکرار رمز ورود</label>
                        <UInput messageClassName={styles.inputErrorMessage} className={`${styles.textInput} ${styles.engTextInput}`} 
                                type="password" name={FieldsName.confirmPassword} placeholder="confirm password" />
                    </div>
                </div>
                <USubmit className={styles.formBtn} okClassName={styles.enableFormBtn} errorClassName={styles.disableFormBtn}>ثبت نام</USubmit>
                <Link className={styles.alreadyHaveAccountLink} to={MainUrl.loginPage}>اکانت دارید؟ از اینجا وارد شوید.</Link>
            </UForm>
        </div>
    )
}

export default SignupForm;
