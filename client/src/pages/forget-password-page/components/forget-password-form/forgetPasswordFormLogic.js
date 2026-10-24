import API from 'api/api'
import { toast } from 'react-toastify';
import { Validations } from 'shared-components/u-form';
import ForgetPasswordResponseStatuses from 'const/forgetPasswordResponseStatuses';

const fetchResetLinkAsync = async (url, email) => {
    const response = await fetch(url, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email: email })
    });

    const data = await response.json();
    return data;
};

export const sendPasswordResetAsync = async (email) => {
    const url = API.postForgetPassword;

    try {
        const data = await fetchResetLinkAsync(url, email);

        if (data.statusCode === ForgetPasswordResponseStatuses.emailSent) {
            toast.success("ایمیل برای شما ارسال گردید.", {
                position: 'top-left',
                autoClose: 3000,
            });

            await new Promise(resolve => setTimeout(resolve, 3000));
        
        } else {
            toast.error('خطایی رخ داد', {
                position: 'top-left',
                autoClose: 5000,
            });
        }
    } catch (error) {
        toast.error('خطا در اتصال سرور', {
			position: 'top-left',
			autoClose: 5000,
		});
    }
};

export const FieldsName = Object.freeze({
	email: "email"
});

const emailValidations = [
	{validator: Validations.required, message: "ایمیل الزامی است", inputNames: [FieldsName.email]},
    {validator: Validations.emailFormat, message: "فرمت ایمیل صحیح نمیباشد", inputNames: [FieldsName.email]},
    {validator: Validations.maxLen(50), message: "ایمیل نمیتواند بیشتر از 50 کاراکتر باشد", inputNames: [FieldsName.email]},
]

export const FieldsValidations = Object.freeze({
    [FieldsName.email]: emailValidations,
});
