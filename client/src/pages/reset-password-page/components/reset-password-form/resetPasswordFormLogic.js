import API from 'api/api'
import { toast } from 'react-toastify';
import { Validations } from 'shared-components/u-form';

const fetchResetPasswordAsync = async (email, token, password) => {
    const url = API.postResetPassword;
    const response = await fetch(url, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ 
            email: email,
            resetCode: token,
            newPassword: password
        })
    });

    const data = await response.json();
    return data;
};

export const resetPasswordAsync = async (email, token, password) => {
    try {
        const data = await fetchResetPasswordAsync(email, token, password);

        if (data.statusCode === 4001) {
            toast.success("موفقیت آمیز بود", {
                position: 'top-left',
                autoClose: 5000,
            });
        } 
        else {
            toast.error('خطایی رخ داد', {
                position: 'top-left',
                autoClose: 5000,
            });
        }
    } 
    catch (error) {
        toast.error('خطا در اتصال سرور', {
			position: 'top-left',
			autoClose: 5000,
		});
    }
};

export const FieldsName = Object.freeze({
	newPassword: "newPassword",
    confirmNewPassword: "confirmNewPassword"
});

const newPasswordValidations = [
	{validator: Validations.required, message: "رمز عبور جدید الزامی است", inputNames: [FieldsName.newPassword]},
    {validator: Validations.minLen(6), message: "طول رمز عبور جدید باید بیشتر از 6 کاراکتر باشد", inputNames: [FieldsName.newPassword]},
    {validator: Validations.strongPassword, message: "رمز عبور جدید حداقل باید شامل 1 حرف کوچک، بزرگ، عدد و کاراکتر خاص (!, @, #, ...) باشد", 
                inputNames: [FieldsName.newPassword]}
]

const confirmNewPasswordValidations = [
	{validator: Validations.required, message: "تکرار رمز عبور جدید الزامی است", inputNames: [FieldsName.confirmNewPassword]},
    {validator: Validations.isSame, message: "تکرار رمز عبور جدید با رمز عبور جدید تطابق ندارد", 
                inputNames: [FieldsName.newPassword, FieldsName.confirmNewPassword]}
]

export const FieldsValidations = Object.freeze({
    [FieldsName.newPassword]: newPasswordValidations,
    [FieldsName.confirmNewPassword]: confirmNewPasswordValidations
});
