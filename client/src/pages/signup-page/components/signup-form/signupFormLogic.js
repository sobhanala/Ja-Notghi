import Api from "api/api"
import { MainUrl } from "routes";
import { toast } from 'react-toastify';
import { Validations } from "shared-components/u-form";
import SignupResponseStatuses from "const/signupResponseStatuses"

const fetchSignupUserAsync = async (signupData) => {
    const url = Api.postSignup

    let response, responseJson;
    try {
        response = await fetch(url, {
            method: "post",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                firstname: signupData.firstname,
                lastname: signupData.lastname,
                username: signupData.username,
                email: signupData.email,
                password: signupData.password,
                dateOfBirth: signupData.dateOfBirth,
                gender: signupData.gender
            }),
        })
        responseJson = await response.json()
    } 
    catch (error) {
        toast.error('خطا در اتصال سرور.', {
			position: 'top-left',
			autoClose: 5000,
		});
        return false;
    }

    if (responseJson.statusCode === SignupResponseStatuses.userAlreadyExists) {
        toast.error('نام کاربری یا ایمیل شما تکراری است.', {
            position: 'top-left',
            autoClose: 5000,
        });
        return false;
    } 
    else if(responseJson.statusCode === SignupResponseStatuses.signupSuccess) {
        toast.success('ثبت نام موفقیت آمیز بود.', {
            position: 'top-left',
            autoClose: 3000,
        });

        await new Promise(resolve => setTimeout(resolve, 3000));

        return true;
    } 
    else if (responseJson.statusCode === SignupResponseStatuses.internalServerError) {
        toast.error('خطا در اتصال سرور.', {
			position: 'top-left',
			autoClose: 5000,
		});
		return false;
    }
    else if (responseJson.statusCode === SignupResponseStatuses.invalidDateOfBirth) {
        toast.error('تاریخ تولد شما معتبر نیست.', {
			position: 'top-left',
			autoClose: 5000,
		});
		return false;
    }
    else {
		throw new Error("Unexpected status code received");
    }
};

export const signupUserAsync = async (signupData, navigateFunc) => {
    const isSuccess = await fetchSignupUserAsync(signupData);
    if (isSuccess) 
        navigateFunc(MainUrl.loginPage)
};

export const formatDate = (date) => {
    if (!(date instanceof Date)) {
        date = new Date(date);
    }

    const year = date.getFullYear()
    const month = String(date.getMonth() + 1).padStart(2, '0')
    const day = String(date.getDate()).padStart(2, '0')

    return `${year}-${month}-${day}`
};

export const FieldsName = Object.freeze({
    firstName: 'firstname',
    lastName: 'lastname',
    birthDate: 'dateOfBirth',
    gender: 'gender',
    email: 'email',
    userName: 'username',
    password: 'password',
    confirmPassword: 'confirmPassword'
}); 

const firstNameValidations = [
    {validator: Validations.required, message: "نام الزامی است", inputNames: [FieldsName.firstName]},
    {validator: Validations.maxLen(50), message: "نام نمیتواند بیشتر از 50 کاراکتر باشد", inputNames: [FieldsName.firstName]},
]

const lastNameValidations = [
    {validator: Validations.required, message: "نام خانوادگی الزامی است", inputNames: [FieldsName.lastName]},
    {validator: Validations.maxLen(50), message: "نام خانوادگی نمیتواند بیشتر از 50 کاراکتر باشد", inputNames: [FieldsName.lastName]},
]

const birthDateValidations = [
    {validator: Validations.required, message: "تاریخ تولد الزامی است", inputNames: [FieldsName.birthDate]},
    // TODO: adding birthDate format validator
]

const genderValidations = [
    {validator: Validations.required, message: "جنسیت الزامی است", inputNames: [FieldsName.gender]}
]

const emailValidations = [
    {validator: Validations.required, message: "ایمیل الزامی است", inputNames: [FieldsName.email]},
    {validator: Validations.emailFormat, message: "فرمت ایمیل صحیح نمیباشد", inputNames: [FieldsName.email]},
    {validator: Validations.maxLen(50), message: "ایمیل نمیتواند بیشتر از 50 کاراکتر باشد", inputNames: [FieldsName.email]},
]

const userNameValidations = [
    {validator: Validations.required, message: "نام کاربری الزامی است", inputNames: [FieldsName.userName]},
    {validator: Validations.maxLen(50), message: "نام کاربری نمیتواند بیشتر از 50 کاراکتر باشد", inputNames: [FieldsName.userName]},
]

const passwordValidations = [
    {validator: Validations.required, message: " رمز ورود الزامی است", inputNames: [FieldsName.password]},
    {validator: Validations.minLen(6), message: "طول رمز ورود باید بیشتر از 6 کاراکتر باشد", inputNames: [FieldsName.password]},
    {validator: Validations.strongPassword, message: "رمز ورود حداقل باید شامل 1 حرف کوچک، بزرگ، عدد و کاراکتر خاص (!, @, #, ...) باشد", 
                inputNames: [FieldsName.password]},
]

const confirmPasswordValidations = [
    {validator: Validations.required, message: "تکرار رمز ورود الزامی است", inputNames: [FieldsName.confirmPassword]},
    {validator: Validations.isSame, message: "تکرار رمز عبور با رمز عبور تطابق ندارد", 
                inputNames: [FieldsName.password, FieldsName.confirmPassword]},
]

export const FieldsValidations = Object.freeze({
    [FieldsName.firstName]: firstNameValidations,
    [FieldsName.lastName]: lastNameValidations,
    //TODO: add after adding feature to UForm component
    // [FieldsName.birthDate]: birthDateValidations,
    // [FieldsName.gender]: genderValidations,
    [FieldsName.email]: emailValidations,
    [FieldsName.userName]: userNameValidations,
    [FieldsName.password]: passwordValidations,
    [FieldsName.confirmPassword]: confirmPasswordValidations
});
