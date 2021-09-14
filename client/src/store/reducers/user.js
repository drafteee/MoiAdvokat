import { userConstants } from "../constants/user.constants";
import notice from "../../components/Notice";
import isRu from "../../helpers/isRu";
import history from "../../helpers/history";

const initialState = {
  //login;
  isLoading: false,
  isLoginLoading: false,
  loginError: null,
  user: undefined,
  roles: [],
  userTypeName: undefined,
  //unblock;
  unblockLoading: false,
  unblockSuccess: null,
  unblockError: null,
  //confirmPhone;
  confirmPhoneLoading: false,
  confirmPhoneSuccess: null,
  confirmPhoneError: null,
  //register;
  registerLoading: false,
  registerSuccess: null,
  registerError: null,
  preCheckRegisterLoading: false,
  preCheckRegisterSuccess: null,
  preCheckRegisterError: null,
  //getUserTypes
  userTypes: [],
  getUserTypesError: null,
  //changeProfileData
  isChangedProfileData: false,
  changeProfileDataError: null,
  changeProfileDataLoading: false,
  //registerByAdmin;
  registerByAdminLoading: false,
  registerByAdminSuccess: null,
  registerByAdminError: null,
  //registerByExternalAdmin;
  registerByExternalAdminLoading: false,
  registerByExternalAdminSuccess: null,
  registerByExternalAdminError: null,
  //getRolesDto
  rolesVM: [],
  functionsVM: [],
  getRolesError: null,
};

export default function(state = initialState, action) {
  //login;
  switch (action.type) {
    case userConstants.Login.REQUEST:
    case userConstants.BelpochtaLogin.REQUEST:
      return {
        ...state,
        isLoginLoading: true,
        loginError: null,
      };
    case userConstants.Login.SUCCESS:
    case userConstants.BelpochtaLogin.SUCCESS:
      console.log(action.payload.result);
      let user = action.payload.result.Output;
      window.localStorage.setItem(
        "user",
        JSON.stringify({
          userName: user.UserName,
          token: user.Token,
          refreshToken: user.RefreshToken,
          sessionId: user.SessionId,
        })
      );
      // window.location.href = '/account'
      const { RefreshToken, Token, SessionId, ...otherUserProps } = user;
      return {
        ...state,
        isLoginLoading: false,
        user: otherUserProps,
        roles: user.roles,
        userTypeName: user?.userType?.name,
      };
    case userConstants.Login.FAILURE:
    case userConstants.BelpochtaLogin.FAILURE:
      return {
        ...state,
        isLoginLoading: false,
        loginError: action.payload.err.data,
      };
    case userConstants.ClearLoginData:
      return {
        isLoading: false,
        isLoginLoading: false,
        loginError: null,
        user: undefined,
        roles: [],
        userTypeName: undefined,
      };
    //signLogin;
    case userConstants.SignLogin.REQUEST:
      return {
        ...state,
        isLoading: true,
        Error: null,
      };
    case userConstants.SignLogin.SUCCESS:
      user = action.payload.result;
      window.localStorage.setItem(
        "user",
        JSON.stringify({
          userName: user.userName,
          token: user.token,
          refreshToken: user.refreshToken,
          sessionId: user.sessionId,
        })
      );
      //window.location.href = '/account'
      return {
        ...state,
        isLoading: false,
        user: user,
        roles: user.roles,
        userTypeName: user?.userType?.name,
      };
    case userConstants.SignLogin.FAILURE:
      notice("error", action.payload.err.data.errors);
      return {
        ...state,
        isLoading: false,
        Error: action.payload.err.data,
      };
    case userConstants.ChangeSign.REQUEST:
      return {
        ...state,
        isChangeSign: false,
        changeSignError: null,
        changeSignLoading: true,
      };
    case userConstants.ChangeSign.SUCCESS:
      notice("success", "ЭЦП успешно прикреплено");
      return {
        ...state,
        isChangeSign: action.payload.result,
        changeSignError: null,
        changeSignLoading: false,
      };
    case userConstants.ChangeSign.FAILURE:
      notice("error", action.payload.err.data.errors);
      return {
        ...state,
        isChangeSign: false,
        changeSignError: action.payload.err,
        changeSignLoading: false,
      };
    //refreshUserData;
    case userConstants.RefreshUserData.REQUEST:
      return {
        ...state,
        isLoading: true,
      };
    case userConstants.RefreshUserData.SUCCESS:
      user = action.payload.result;
      if (!user) {
        user = null;
      }
      return {
        ...state,
        isLoading: false,
        user: user.Output,
        roles: user.Output ? user.Output.roles : [],
        userTypeName:
          user.Output && user.Output.userType
            ? user.Output.userType.name
            : undefined,
      };
    case userConstants.RefreshUserData.FAILURE:
      return {
        ...state,
        isLoading: false,
        error: action.err,
      };
    //logout;
    case userConstants.Logout.REQUEST:
      return {
        ...state,
        isLoading: true,
      };
    case userConstants.Logout.SUCCESS:
      // window.location.href = '/'
      //history.replace('/')
      window.localStorage.removeItem("user");
      return {
        ...state,
        isLoading: false,
        user: undefined,
        roles: [],
        userTypeName: undefined,
      };
    case userConstants.Logout.FAILURE:
      return {
        ...state,
        isLoading: false,
        user: null,
        roles: [],
        userTypeName: undefined,
        error: action.err,
      };
    //unblock;
    case userConstants.Unblock.REQUEST:
      return {
        ...state,
        unblockLoading: true,
        unblockSuccess: null,
        unblockError: null,
      };
    case userConstants.Unblock.SUCCESS:
      return {
        ...state,
        unblockLoading: false,
        unblockSuccess: action.payload.result,
        unblockError: null,
      };
    case userConstants.Unblock.FAILURE:
      return {
        ...state,
        unblockLoading: false,
        unblockSuccess: null,
        unblockError: action.payload.err,
      };
    //confirmPhone;
    case userConstants.ConfirmPhone.REQUEST:
      return {
        ...state,
        confirmPhoneLoading: true,
        confirmPhoneSuccess: null,
        confirmPhoneError: null,
      };
    case userConstants.ConfirmPhone.SUCCESS:
      return {
        ...state,
        confirmPhoneLoading: false,
        confirmPhoneSuccess: action.payload.result,
        confirmPhoneError: null,
      };
    case userConstants.ConfirmPhone.FAILURE:
      return {
        ...state,
        confirmPhoneLoading: false,
        confirmPhoneSuccess: null,
        confirmPhoneError: action.payload.err,
      };
    //register;
    case userConstants.Register.REQUEST:
      return {
        ...state,
        registerLoading: true,
        registerSuccess: null,
        registerError: null,
      };
    case userConstants.Register.SUCCESS:
      return {
        ...state,
        registerLoading: false,
        registerSuccess: action.payload.result,
      };
    case userConstants.Register.FAILURE:
      ////console.log('actionReg', action.payload)
      notice("error", action.payload.err.data.errors);
      return {
        ...state,
        pre: false,
        registerError: action.payload.err,
      };

    case userConstants.PreCheckRegister.REQUEST:
      return {
        ...state,
        preCheckRegisterLoading: true,
        preCheckRegisterSuccess: null,
        registerError: null,
      };
    case userConstants.PreCheckRegister.SUCCESS:
      return {
        ...state,
        preCheckRegisterLoading: false,
        preCheckRegisterSuccess: action.payload.result,
      };
    case userConstants.PreCheckRegister.FAILURE:
      ////console.log('actionReg', action.payload)
      notice("error", action.payload.err.data.errors);
      return {
        ...state,
        preCheckRegisterLoading: false,
        preCheckRegisterError: action.payload.err,
      };
    //getUserTypes
    case userConstants.GetUserTypes.REQUEST:
      return {
        ...state,
        getUserTypesError: null,
      };
    case userConstants.GetUserTypes.SUCCESS:
      return {
        ...state,
        userTypes: action.payload.result,
      };
    case userConstants.GetUserTypes.FAILURE:
      notice("error", action.payload.err.data.errors);
      return {
        ...state,
        getUserTypesError: action.payload.err,
      };
    case userConstants.ChangeProfileData.REQUEST:
      return {
        ...state,
        isChangedProfileData: false,
        changeProfileDataError: null,
        changeProfileDataLoading: true,
      };
    case userConstants.ChangeProfileData.SUCCESS:
      return {
        ...state,
        isChangedProfileData: action.payload.result,
        changeProfileDataError: null,
        changeProfileDataLoading: false,
      };
    case userConstants.ChangeProfileData.FAILURE:
      notice("error", action.payload.err.data.errors);
      return {
        ...state,
        isChangedProfileData: false,
        changeProfileDataError: action.payload.err,
        changeProfileDataLoading: false,
      };
    //registerByAdmin;
    case userConstants.RegisterByAdmin.REQUEST:
      return {
        ...state,
        registerByAdminLoading: true,
        registerByAdminSuccess: null,
        registerByAdminError: null,
      };
    case userConstants.RegisterByAdmin.SUCCESS:
      return {
        ...state,
        registerByAdminLoading: false,
        registerByAdminSuccess: action.payload.result,
      };
    case userConstants.RegisterByAdmin.FAILURE:
      notice("error", action.payload.err.data.errors);
      return {
        ...state,
        registerByAdminLoading: false,
        registerByAdminError: action.payload.err,
      };
    //registerByExternalAdmin;
    case userConstants.RegisterByExternalAdmin.REQUEST:
      return {
        ...state,
        registerByExternalAdminLoading: true,
        registerByExternalAdminSuccess: null,
        registerByExternalAdminError: null,
      };
    case userConstants.RegisterByExternalAdmin.SUCCESS:
      return {
        ...state,
        registerByExternalAdminLoading: false,
        registerByExternalAdminSuccess: action.payload.result,
      };
    case userConstants.RegisterByExternalAdmin.FAILURE:
      notice("error", action.payload.err.data.errors);
      return {
        ...state,
        registerByExternalAdminLoading: false,
        registerByExternalAdminError: action.payload.err,
      };
    //getRolesDto
    case userConstants.GetRoles.REQUEST:
      return {
        ...state,
        rolesVM: [],
      };
    case userConstants.GetRoles.SUCCESS:
      return {
        ...state,
        rolesVM: action.payload.result.Output,
      };
    case userConstants.GetRoles.FAILURE:
      notice("error", action.payload.err.data.errors);
      return {
        ...state,
        getRolesError: action.payload.err,
      };
    case userConstants.GetFunctions.REQUEST:
      return {
        ...state,
        functionsVM: [],
      };
    case userConstants.GetFunctions.SUCCESS:
      return {
        ...state,
        functionsVM: action.payload.result.Output,
      };
    case userConstants.GetFunctions.FAILURE:
      notice("error", action.payload.err.data.errors);
      return {
        ...state,
      };
    case userConstants.UpdateRoleFunctions.REQUEST:
      return {
        ...state,
      };
    case userConstants.UpdateRoleFunctions.SUCCESS:
      return {
        ...state,
      };
    case userConstants.UpdateRoleFunctions.FAILURE:
      notice("error", action.payload.err.data.errors);
      return {
        ...state,
      };
    default:
      return state;
  }
}
