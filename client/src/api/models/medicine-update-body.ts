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
/**
 * 
 * @export
 * @interface MedicineUpdateBody
 */
export interface MedicineUpdateBody {
    /**
     * 
     * @type {string}
     * @memberof MedicineUpdateBody
     */
    id: string;
    /**
     * 
     * @type {number}
     * @memberof MedicineUpdateBody
     */
    price?: number;
    /**
     * 
     * @type {number}
     * @memberof MedicineUpdateBody
     */
    count?: number;
    /**
     * 
     * @type {Blob}
     * @memberof MedicineUpdateBody
     */
    image?: Blob;
}
