
export class NumberUtils {

  private constructor() { }
/**
 * Indicates if the provided number is between
 * the minimum and maximum value
 *
 * @static
 * @param {number} value - the number to compare
 * @param {number} minimum - the minimum value in the range
 * @param {number} maximum - the maximum value in the range
 * @param {true} inclusive - true if the minimum and maximum values
 * are inclusively compared
 * @return {boolean} true if value is within range
 * @memberof NumberUtils
 */
public static isInRange(
    value: number,
    minimum: number,
    maximum: number,
    inclusive: boolean = true
  ): boolean {

    if (minimum > maximum) {
      throw new Error("Minimum value cannot be greater than the maximum value")
    }

    if (inclusive) {
      return value >= minimum && value <= maximum;
    }
    return value > minimum && value < maximum;
  }
}
