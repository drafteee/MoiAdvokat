function ConvertDBAddressToString(Address, isRu){
	let strAddress = ''
	if (isRu){
		if (Address){
			if (Address.postIndex)
				strAddress += Address.postIndex
			if (Address.reAdministrativeTerritory){
				strAddress = nullCheckAndAdd(strAddress)
				strAddress += Address.reAdministrativeTerritory.reRegion ? `${ Address.reAdministrativeTerritory.reRegion.name } обл., ` : ''
				strAddress += Address.reAdministrativeTerritory.reDistrict ? `${ Address.reAdministrativeTerritory.reDistrict.name } р-н., ` : ''
				strAddress += !Address.reAdministrativeTerritory.reLocalityType.position ?
					`${ Address.reAdministrativeTerritory.reLocalityType.nameShort } ${ Address.reAdministrativeTerritory.name }`
					: `${ Address.reAdministrativeTerritory.name } ${ Address.reAdministrativeTerritory.reLocalityType.nameShort }`
			}
			if (Address.urbanAreaName)
				strAddress += `, ${ Address.urbanAreaNameBel }`
			if (Address.reAddress){
				strAddress = nullCheckAndAdd(strAddress)
				strAddress += Address.reAddress.reAddressType ?
					Address.reAddress.reAddressType.nameShort ? Address.reAddress.reAddressType.nameShort : Address.reAddress.reAddressType.name
					: ''
				strAddress += ` ${ Address.reAddress.name }`
			}
			if (Address.reCountry){
				strAddress = nullCheckAndAdd(strAddress)
				strAddress += Address.reCountry.name
			}
			if (Address.notice){
				strAddress = nullCheckAndAdd(strAddress)
				strAddress += Address.notice
			}
			strAddress = Address.house ? `${ nullCheckAndAdd(strAddress) }д. ${ Address.house }` : strAddress
			strAddress = Address.office ? `${ nullCheckAndAdd(strAddress) }кв. ${ Address.office }` : strAddress
		}
	}
	else if (Address){
		if (Address.postIndex)
			strAddress += Address.postIndex
		if (Address.reAdministrativeTerritory){
			strAddress = nullCheckAndAdd(strAddress)
			strAddress += Address.reAdministrativeTerritory.reRegion ? `${ Address.reAdministrativeTerritory.reRegion.nameBel } вобл., ` : ''
			strAddress += Address.reAdministrativeTerritory.reDistrict ? `${ Address.reAdministrativeTerritory.reDistrict.nameBel } р-н., ` : ''
			const reAdmType = Address.reAdministrativeTerritory.reLocalityType.nameShortBel ?
				Address.reAdministrativeTerritory.reLocalityType.nameShortBel :
				Address.reAdministrativeTerritory.reLocalityType.nameBel ?
					Address.reAdministrativeTerritory.reLocalityType.nameBel :
					Address.reAdministrativeTerritory.reLocalityType.nameShort ?
						Address.reAdministrativeTerritory.reLocalityType.nameShort :
						Address.reAdministrativeTerritory.reLocalityType.name
			strAddress += !Address.reAdministrativeTerritory.reLocalityType.position ?
				`${ reAdmType } ${ Address.reAdministrativeTerritory.nameBel }`
				: `${ Address.reAdministrativeTerritory.nameBel } ${ reAdmType }`
		}
		if (Address.urbanAreaNameBel)
			strAddress += `, ${ Address.urbanAreaNameBel }`
		if (Address.reAddress){
			strAddress = nullCheckAndAdd(strAddress)
			strAddress +=
                    Address.reAddress.reAddressType ?
                    	Address.reAddress.reAddressType.nameShortBel ?
                    		Address.reAddress.reAddressType.nameShortBel :
                    		Address.reAddress.reAddressType.nameBel ?
                    			Address.reAddress.reAddressType.nameBel :
                    			Address.reAddress.reAddressType.nameShort ?
                    				Address.reAddress.reAddressType.nameShort :
                    				Address.reAddress.reAddressType.name
                    	: ''
			strAddress += ` ${ Address.reAddress.nameBel ? Address.reAddress.nameBel : Address.reAddress.name }`
		}
		strAddress = Address.house ? `${ nullCheckAndAdd(strAddress) }д. ${ Address.house }` : strAddress
		strAddress = Address.office ? `${ nullCheckAndAdd(strAddress) }кв. ${ Address.office }` : strAddress
	}

	return strAddress
}

function nullCheckAndAdd(str){
	if (str)
		return `${ str }, `

	return str
}
export default ConvertDBAddressToString