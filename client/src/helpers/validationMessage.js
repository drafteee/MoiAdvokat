import i18nGlobal from "../localization";

function validationMessage(formItem, isRu) {
    return `${i18nGlobal.pleaseFillMessage[isRu]} ${formItem[isRu]}`
}

export default validationMessage