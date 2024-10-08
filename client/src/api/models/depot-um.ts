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
 * @interface DepotUM
 */
export interface DepotUM {
    /**
     * 
     * @type {string}
     * @memberof DepotUM
     */
    id: string;
    /**
     * 
     * @type {string}
     * @memberof DepotUM
     */
    name?: string | null;
    /**
     * 
     * @type {string}
     * @memberof DepotUM
     */
    managerId?: string | null;
    /**
     * 
     * @type {Address}
     * @memberof DepotUM
     */
    address?: Address;
}
