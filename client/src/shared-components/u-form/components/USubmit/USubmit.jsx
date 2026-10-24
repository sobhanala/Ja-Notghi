const USubmit = ({children, type, className, errorClassName, okClassName, isOk, ...restProps}) => {
    return (
        <button type="submit" className={`${className} ${isOk ? okClassName : errorClassName}`}
             {...restProps}>{children}</button>
    )
}

export default USubmit;
