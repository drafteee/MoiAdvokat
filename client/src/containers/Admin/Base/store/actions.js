import api from "./api";
import { baseConstants } from "./constants";
import { defAction } from "../../../../helpers/defaultAction";

export const baseActions = {
    getAll,
    createOrUpdate,
    softDelete,
    restore
}

function getAll(controller) {
    const dispatchObj = {
        constants: baseConstants.GetAll,
        service: {
            func: api.getAll,
            params: {
                controller
            }
        }
    };

    return defAction(dispatchObj);
}

function createOrUpdate(params) {
    const dispatchObj = {
        constants: baseConstants.CreateOrUpdate,
        service: {
            func: api.createOrUpdate,
            params
        }
    };

    return defAction(dispatchObj);
}

function softDelete(params) {
    const dispatchObj = {
        constants: baseConstants.SoftDelete,
        service: {
            func: api.softDelete,
            params
        }
    }

    return defAction(dispatchObj)
}

function restore(params) {
    const dispatchObj = {
        constants: baseConstants.Restore,
        service: {
            func: api.restore,
            params
        }
    }

    return defAction(dispatchObj)
}
