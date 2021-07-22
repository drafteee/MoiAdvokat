function ConvertAddressToDBAddress(values) {
    return {
        ReAddressId: values.Address ? values.Address.id : null,
        House: values.House,
        Office: values.Office,
        ReAdministrativeTerritoryId: values.AdministrativeTerritory ? values.AdministrativeTerritory.id : null,
        PostIndex: values.postIndex,
        Notice: values.Notice ? values.Notice : null,
        ReCountryId: values.ReCountry ? values.ReCountry.id : null
    }
}
export default ConvertAddressToDBAddress;