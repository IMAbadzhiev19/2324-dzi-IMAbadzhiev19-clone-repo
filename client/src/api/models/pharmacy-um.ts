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
/**
 * 
 * @export
 * @interface PharmacyUM
 */
export interface PharmacyUM {
    /**
     * 
     * @type {string}
     * @memberof PharmacyUM
     */
    id: string;
    /**
     * 
     * @type {string}
     * @memberof PharmacyUM
     */
    name?: string | null;
    /**
     * 
     * @type {string}
     * @memberof PharmacyUM
     */
    description?: string | null;
    /**
     * 
     * @type {Address}
     * @memberof PharmacyUM
     */
    address?: Address;
    /**
     * 
     * @type {string}
     * @memberof PharmacyUM
     */
    founderId?: string | null;
    /**
     * 
     * @type {string}
     * @memberof PharmacyUM
     */
    depotId?: string | null;
}
