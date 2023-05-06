export interface Column {
  title: string;
  dataKey: string;
  align?: 'right' | 'left' | 'center';
  isSortable?: boolean;
}
