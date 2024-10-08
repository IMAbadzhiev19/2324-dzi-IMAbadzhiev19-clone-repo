/* tslint:disable */
/* eslint-disable */
/**
 * PMS.Api
 * Pharmacy Management System API
 *
 * OpenAPI spec version: v1
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { Address } from './address';
import { MedicineVM } from './medicine-vm';
import { UserVM } from './user-vm';
/**
 * 
 * @export
 * @interface DepotVM
 */
export interface DepotVM {
    /**
     * 
     * @type {string}
     * @memberof DepotVM
     */
    id?: string | null;
    /**
     * 
     * @type {string}
     * @memberof DepotVM
     */
    name?: string | null;
    /**
     * 
     * @type {Address}
     * @memberof DepotVM
     */
    address?: Address;
    /**
     * 
     * @type {UserVM}
     * @memberof DepotVM
     */
    manager?: UserVM;
    /**
     * 
     * @type {Array<MedicineVM>}
     * @memberof DepotVM
     */
    medicines?: Array<MedicineVM> | null;
    /**
     * 
     * @type {string}
     * @memberof DepotVM
     */
    createdBy?: string | null;
    /**
     * 
     * @type {Date}
     * @memberof DepotVM
     */
    createdOn?: Date;
    /**
     * 
     * @type {string}
     * @memberof DepotVM
     */
    updatedBy?: string | null;
    /**
     * 
     * @type {Date}
     * @memberof DepotVM
     */
    updatedOn?: Date;
}
