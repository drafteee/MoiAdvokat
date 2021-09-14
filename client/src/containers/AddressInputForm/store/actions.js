import api from "./api";
import { addressConstants } from "./constants";
import { defAction } from "../../../helpers/defaultAction";

export const addressActions = {
    getCountries,
    getAdministrativeTerritories
}

function getCountries() {
    const dispatchObj = {
        constants: addressConstants.GetCountries,
        service: {
            func: api.getCountries
        }
    }

    return defAction(dispatchObj)
}

function getAdministrativeTerritories(params) {
    const dispatchObj = {
        constants: addressConstants.GetAdministrativeTerritories,
        service: {
            func: api.getAdministrativeTerritories,
            params
        }
    };

    return defAction(dispatchObj)
}
