import Api from "api/api"
import { toast } from 'react-toastify';
import { setToken } from "api/services"
import { Validations } from "shared-components/u-form";

const identifierEmailValidator = ([identifier]) => {
	return isIdentifierEmail(identifier) ? 
		   Validations.emailFormat([identifier]) : true;
}  

const isIdentifierEmail = (identifier) => {
	return identifier.includes("@");
}

const createLoginBody = (formData) => {
	const body = {
		identifier: formData.identifier,
		password: formData.password,
		isEmail: isIdentifierEmail(formData.identifier)
	};
	return body;
}

const fetchLoginUserAsync = async (body) => {
	const url = Api.postLogin

	try {
		const response = await fetch(url, {
			method: "post",
			headers: {"Content-Type": "application/json"},
			body: JSON.stringify(body),
		})

		const data = await response.json()
		return data
	} 
	catch (error) {
		toast.error('خطا در اتصال سرور.', {
			position: 'top-left',
			autoClose: 5000,
		});
		return ""
	}
}

export const loginUserAsync = async (loginFormData) => {
	const loginBody = createLoginBody(loginFormData);
	const data = await fetchLoginUserAsync(loginBody);

	const token = data.accessToken;
	//TODO: check the token then apis are ready
	// console.log(token);
	setToken(token);
}

export const FieldsName = Object.freeze({
	identifier: "identifier",
	password: "password"
});

const identifierValidations = [
	{validator: Validations.required, message: "نام کاربری یا ایمیل نمی تواند خالی باشد", inputNames: [FieldsName.identifier]},
	{validator: Validations.maxLen(50), message: "نام کاربری یا ایمیل نمی تواند از 50 کاراکتر بیشتر باشد", 
				inputNames: [FieldsName.identifier]},
	{validator: identifierEmailValidator, message: "فرمت ایمیل صحیح نمیباشد", inputNames: [FieldsName.identifier]},
]

const passwordValidations = [
	{validator: Validations.required, message: "رمز عبور نمی تواند خالی باشد", inputNames: [FieldsName.password]}
]

export const FieldsValidations = Object.freeze({
	[FieldsName.identifier]: identifierValidations,
	[FieldsName.password]: passwordValidations
});
