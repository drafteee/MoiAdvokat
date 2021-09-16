import {
    baseConstants
} from './constants'

const initialState =
{
    loading: false,
    currentList: null,
    currentObject: null,
    createOrUpdateSuccess: null,
    error: null,
    updateTrigger: 0
}

export default function (state = initialState, action) {
    switch (action.type) {
        case baseConstants.GetAll.REQUEST: {
            return {
                ...state,
                loading: true,
                currentList: null
            }
        }
        case baseConstants.GetAll.SUCCESS: {
            return {
                ...state,
                loading: false,
                currentList: action.payload.result
            }
        }
        case baseConstants.GetAll.FAILURE: {
            return {
                ...state,
                loading: false,
                error: action.payload
            }
        }

        case baseConstants.CreateOrUpdate.REQUEST: {
            return {
                ...state,
                loading: true,
                createOrUpdateSuccess: null
            }
        }
        case baseConstants.CreateOrUpdate.SUCCESS: {
            return {
                ...state,
                loading: false,
                createOrUpdateSuccess: true,
                updateTrigger: state.updateTrigger + 1
            }
        }
        case baseConstants.CreateOrUpdate.FAILURE: {
            return {
                ...state,
                loading: false,
                error: action.payload,
                createOrUpdateSuccess: false
            }
        }

        case baseConstants.SoftDelete.REQUEST: {
            return {
                ...state
            }
        }
        case baseConstants.SoftDelete.SUCCESS: {
            return {
                ...state,
                updateTrigger: state.updateTrigger + 1
            }
        }
        case baseConstants.SoftDelete.FAILURE: {
            return {
                ...state
            }
        }

        case baseConstants.Restore.REQUEST: {
            return {
                ...state
            }
        }
        case baseConstants.Restore.SUCCESS: {
            return {
                ...state,
                updateTrigger: state.updateTrigger + 1
            }
        }
        case baseConstants.Restore.FAILURE: {
            return {
                ...state
            }
        }

        default:
            return state
    }
}