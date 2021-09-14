import {
    addressConstants
} from './constants'

const initialState =
{
    countries: [],
    loadingCountriesError: null,
    administrativeTerritories: [],
    loadingAdministrativeTerritories: null,
}

export default function (state = initialState, action) {
    switch (action.type) {
        case addressConstants.GetCountries.REQUEST: {
            return {
                ...state,
                countries: []
            }
        }
        case addressConstants.GetCountries.SUCCESS: {
            return {
                ...state,
                countries: action.payload.result
            }
        }
        case addressConstants.GetCountries.FAILURE: {
            return {
                ...state,
                loadingCountriesError: action.payload
            }
        }

        case addressConstants.GetAdministrativeTerritories.REQUEST: {
            return {
                ...state,
                administrativeTerritories: []
            }
        }
        case addressConstants.GetAdministrativeTerritories.SUCCESS: {
            return {
                ...state,
                administrativeTerritories: action.payload.result
            }
        }
        case addressConstants.GetAdministrativeTerritories.FAILURE: {
            return {
                ...state,
                loadingAdministrativeTerritories: action.payload
            }
        }

        default:
            return state
    }
}