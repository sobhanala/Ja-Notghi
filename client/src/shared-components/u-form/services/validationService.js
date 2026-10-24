export const required = ([input]) => {
    return input !== null && input !== "";
}

export const emailFormat = ([input]) => {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
	return emailRegex.test(input);
}

export const maxLen = (limit) => ([input]) => {
    const stringInput = String(input);
    return stringInput.length <= limit;
}

export const minLen = (limit) => ([input]) => {
    const stringInput = String(input);
    return stringInput.length >= limit;
}

export const strongPassword = ([password]) => {
    const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W)/;
    return passwordRegex.test(password);
}

export const isSame = ([input1, input2]) => {
    return input1 === input2;
}

const Validations = Object.freeze({
    required: required,
    emailFormat: emailFormat,
    maxLen: maxLen,
    minLen: minLen,
    strongPassword: strongPassword,
    isSame: isSame,
});

export default Validations;
